using Microsoft.EntityFrameworkCore;
using Swapy.BLL.Interfaces;
using Swapy.Common.DTO.Products.Responses;
using Swapy.Common.Entities;
using Swapy.Common.Enums;
using Swapy.Common.Exceptions;
using Swapy.DAL.Interfaces;

namespace Swapy.DAL.Repositories
{
    public class AnimalAttributeRepository : IAnimalAttributeRepository
    {
        private readonly SwapyDbContext _context;

        private readonly IFavoriteProductRepository _favoriteProductRepository;

        private readonly ISubcategoryRepository _subcategoryRepository;

        private readonly ICurrencyRepository _currencyRepository;

        private readonly ICurrencyConverterService _currencyConverterService;

        public AnimalAttributeRepository(SwapyDbContext context, IFavoriteProductRepository favoriteProductRepository, ISubcategoryRepository subcategoryRepository, ICurrencyRepository currencyRepository, ICurrencyConverterService currencyConverterService)
        {
            _context = context;
            _favoriteProductRepository = favoriteProductRepository;
            _subcategoryRepository = subcategoryRepository;
            _currencyRepository = currencyRepository;
            _currencyConverterService = currencyConverterService;
        }
        
        public async Task CreateAsync(AnimalAttribute item)
        {
            await _context.AnimalAttributes.AddAsync(item);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(AnimalAttribute item)
        {
            _context.AnimalAttributes.Update(item);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(AnimalAttribute item)
        {
            _context.AnimalAttributes.Remove(item);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteByIdAsync(string id) => await DeleteAsync(await GetByIdAsync(id));

        public async Task<AnimalAttribute> GetByIdAsync(string id)
        {
            var item = await _context.AnimalAttributes.FindAsync(id);
            if (item == null) throw new NotFoundException($"{GetType().Name.Split("Repository")[0]} with {id} id not found");
            return item;
        }

        public async Task<AnimalAttribute> GetByProductIdAsync(string productId)
        {
            var item = await _context.AnimalAttributes.Where(x => x.ProductId.Equals(productId)).FirstOrDefaultAsync();
            if (item == null) throw new NotFoundException($"{GetType().Name.Split("Repository")[0]} with {productId} id not found");
            return item;
        }

        public async Task<IEnumerable<AnimalAttribute>> GetAllAsync()
        {
            return await _context.AnimalAttributes.ToListAsync();
        }

        public async Task<ProductsResponseDTO<ProductResponseDTO>> GetAllFilteredAsync(int page,
                                                                                       int pageSize,
                                                                                       string userId,
                                                                                       string title,
                                                                                       string currencyId,
                                                                                       decimal? priceMin,
                                                                                       decimal? priceMax,
                                                                                       string categoryId,
                                                                                       string subcategoryId,
                                                                                       string cityId,
                                                                                       string otherUserId,
                                                                                       List<string> animalBreedsId,
                                                                                       List<string> animalTypesId,
                                                                                       bool? sortByPrice,
                                                                                       bool? reverseSort)
        {
            if (page < 1 || pageSize < 1) throw new ArgumentException($"Page and page size parameters must be greater than one.");

            List<SpecificationResponseDTO<string>> sequenceOfSubcategories = subcategoryId == null ? new() : (await _subcategoryRepository.GetAllChildsOfSubcategory(subcategoryId)).ToList();

            var query = _context.AnimalAttributes.Include(a => a.Product)
                                                    .ThenInclude(p => p.Currency)
                                                 .Include(a => a.AnimalBreed)
                                                 .AsQueryable();

            decimal? minPrice = currencyId == null ? null : Math.Floor((await query.ToListAsync()).Select(x => _currencyConverterService.Convert(x.Product.Currency.Name, _currencyRepository.GetById(currencyId).Name, x.Product.Price)).OrderBy(p => p).FirstOrDefault());
            decimal? maxPrice = currencyId == null ? null : Math.Ceiling((await query.ToListAsync()).Select(x => _currencyConverterService.Convert(x.Product.Currency.Name, _currencyRepository.GetById(currencyId).Name, x.Product.Price)).OrderBy(p => p).LastOrDefault());

            var list = await query.Where(x => (title == null || x.Product.Title.Contains(title)) &&
                                (categoryId == null || x.Product.CategoryId.Equals(categoryId)) &&
                                (subcategoryId == null ? true : sequenceOfSubcategories.Select(x => x.Id).Contains(x.Product.SubcategoryId)) &&
                                (cityId == null || x.Product.CityId.Equals(cityId)) &&
                                (otherUserId == null ? !x.Product.UserId.Equals(userId) : x.Product.UserId.Equals(otherUserId)) &&
                                (animalBreedsId == null || animalBreedsId.Contains(x.AnimalBreedId)) &&
                                x.Product.IsDisable.Equals(false) &&
                                (animalTypesId == null || animalTypesId.Contains(x.AnimalBreed.AnimalTypeId)))
                                .Include(a => a.Product)
                                    .ThenInclude(p => p.Subcategory)
                                .Include(a => a.Product)
                                    .ThenInclude(p => p.Images)
                                .Include(a => a.Product)
                                    .ThenInclude(p => p.City)
                                .Include(a => a.Product)
                                    .ThenInclude(p => p.User)
                                .ToListAsync();

            list = list.Where(x => (priceMin == null || currencyId == null || priceMin <= _currencyConverterService.Convert(x.Product.Currency.Name, _currencyRepository.GetById(currencyId).Name, x.Product.Price)) &&
            (priceMax == null || currencyId == null || priceMax >= _currencyConverterService.Convert(x.Product.Currency.Name, _currencyRepository.GetById(currencyId).Name, x.Product.Price))).ToList();

            var count = list.Count;
            if (count <= pageSize * (page - 1)) throw new NotFoundException($"Page {page} not found.");

            if (sortByPrice == true) list = list.OrderBy(x => _currencyConverterService.Convert(x.Product.Currency.Name, "usd", x.Product.Price)).ToList();
            else list = list.OrderBy(x => x.Product.DateTime).ToList();
            if (reverseSort == true) list.Reverse();

            list = list.Skip(pageSize * (page - 1))
                 .Take(pageSize)
                 .ToList();

            var result = list.Select(x => new ProductResponseDTO()
            {
                Id = x.ProductId,
                Title = x.Product.Title,
                Price = x.Product.Price,
                City = x.Product.City.Name,
                Currency = x.Product.Currency.Name,
                CurrencySymbol = x.Product.Currency.Symbol,
                DateTime = x.Product.DateTime,
                Images = x.Product.Images.Select(i => i.Image).ToList(),
                IsDisable = x.Product.IsDisable,
                UserType = x.Product.User.Type,
                UserId = x.Product.UserId,
                Type = x.Product.Subcategory.Type
            }).ToList();

            foreach (var item in result)
            {
                item.IsFavorite = userId == null ? false : await _favoriteProductRepository.CheckProductOnFavorite(item.Id, userId);
            }

            return new ProductsResponseDTO<ProductResponseDTO>(result, count, (int)Math.Ceiling(Convert.ToDouble(count) / pageSize), maxPrice, minPrice);
        }

        public async Task<AnimalAttribute> GetDetailByIdAsync(string productId)
        {
            var item = await _context.AnimalAttributes.Where(a => a.ProductId.Equals(productId))
                                                      .Include(a => a.Product)
                                                        .ThenInclude(p => p.Images)
                                                      .Include(a => a.Product)
                                                        .ThenInclude(p => p.Category)
                                                      .Include(a => a.Product)
                                                        .ThenInclude(p => p.City)
                                                      .Include(a => a.Product)
                                                        .ThenInclude(p => p.Currency)
                                                      .Include(a => a.Product)
                                                        .ThenInclude(p => p.User)
                                                            .ThenInclude(u => u.ShopAttribute)
                                                      .Include(a => a.AnimalBreed)
                                                        .ThenInclude(ab => ab.AnimalType)
                                                      .FirstOrDefaultAsync();

            if (item == null) throw new NotFoundException($"{GetType().Name.Split("Repository")[0]} with {productId} id not found");
            return item;
        }
    }
}
