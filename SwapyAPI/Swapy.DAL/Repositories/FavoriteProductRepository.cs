using Microsoft.EntityFrameworkCore;
using Swapy.BLL.Interfaces;
using Swapy.Common.DTO.Products.Responses;
using Swapy.Common.Entities;
using Swapy.Common.Exceptions;
using Swapy.DAL.Interfaces;

namespace Swapy.DAL.Repositories
{
    public class FavoriteProductRepository : IFavoriteProductRepository
    {
        private readonly SwapyDbContext _context;

        private readonly ISubcategoryRepository _subcategoryRepository;

        private readonly ICurrencyRepository _currencyRepository;

        private readonly ICurrencyConverterService _currencyConverterService;

        public FavoriteProductRepository(SwapyDbContext context, ISubcategoryRepository subcategoryRepository, ICurrencyRepository currencyRepository, ICurrencyConverterService currencyConverterService)
        {
            _context = context;
            _subcategoryRepository = subcategoryRepository;
            _currencyConverterService = currencyConverterService;
            _currencyRepository = currencyRepository;
        }

        public async Task CreateAsync(FavoriteProduct item)
        {
            await _context.FavoriteProducts.AddAsync(item);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(FavoriteProduct item)
        {
            _context.FavoriteProducts.Update(item);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(FavoriteProduct item)
        {
            _context.FavoriteProducts.Remove(item);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteByIdAsync(string id) => await DeleteAsync(await GetByIdAsync(id));

        public async Task<FavoriteProduct> GetByIdAsync(string id)
        {
            var item = await _context.FavoriteProducts.FindAsync(id);
            if (item == null) throw new NotFoundException($"{GetType().Name.Split("Repository")[0]} with {id} id not found");
            return item;
        }

        public async Task<FavoriteProduct> GetByProductAndUserIdAsync(string productId, string userId)
        {
            var item = await _context.FavoriteProducts.Where(x => x.ProductId.Equals(productId) && x.UserId.Equals(userId)).FirstOrDefaultAsync();
            if (item == null) throw new NotFoundException($"{GetType().Name.Split("Repository")[0]} with {productId} id not found");
            return item;
        }

        public async Task<IEnumerable<FavoriteProduct>> GetAllAsync()
        {
            return await _context.FavoriteProducts.ToListAsync();
        }

        public async Task<FavoriteProduct> GetDetailByIdAsync(string id)
        {
            var item = await _context.FavoriteProducts.Where(fp => fp.Id.Equals(id))
                                                      .Include(fp => fp.Product)
                                                        .ThenInclude(p => p.Images)
                                                      .Include(fp => fp.Product)
                                                        .ThenInclude(p => p.City)
                                                      .Include(fp => fp.Product)
                                                        .ThenInclude(p => p.Currency)
                                                      .Include(a => a.Product)
                                                        .ThenInclude(p => p.User)
                                                            .ThenInclude(u => u.ShopAttribute)
                                                      .FirstOrDefaultAsync();

            if (item == null) throw new NotFoundException($"{GetType().Name.Split("Repository")[0]} with {id} id not found");
            return item;
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
                                                                                       string productId,
                                                                                       bool? sortByPrice,
                                                                                       bool? reverseSort)
        {
            if (page < 1 || pageSize < 1) throw new ArgumentException($"Page and page size parameters must be greater than one.");

            List<SpecificationResponseDTO<string>> sequenceOfSubcategories = subcategoryId == null ? new() : (await _subcategoryRepository.GetSequenceOfSubcategories(subcategoryId)).ToList();

            var query = _context.FavoriteProducts.Include(fp => fp.Product)
                                                    .ThenInclude(p => p.Currency)
                                                  .AsQueryable();

            decimal? minPrice = currencyId == null ? null : Math.Floor((await query.ToListAsync()).Select(x => _currencyConverterService.Convert(x.Product.Currency.Name, _currencyRepository.GetById(currencyId).Name, x.Product.Price)).OrderBy(p => p).FirstOrDefault());
            decimal? maxPrice = currencyId == null ? null : Math.Ceiling((await query.ToListAsync()).Select(x => _currencyConverterService.Convert(x.Product.Currency.Name, _currencyRepository.GetById(currencyId).Name, x.Product.Price)).OrderBy(p => p).LastOrDefault());

            var list = await query.Where(x => (title == null || x.Product.Title.Contains(title)) &&
                         (categoryId == null || x.Product.CategoryId.Equals(categoryId)) &&
                         (subcategoryId == null ? true : sequenceOfSubcategories.Select(x => x.Id).Contains(subcategoryId)) &&
                         (cityId == null || x.Product.CityId.Equals(cityId)) &&
                         (otherUserId == null ? x.UserId.Equals(userId) : x.UserId.Equals(otherUserId)) &&
                         (productId == null || x.ProductId.Equals(productId)))
                        .Include(p => p.Product)
                            .ThenInclude(p => p.Images)
                        .Include(p => p.Product)
                            .ThenInclude(p => p.Subcategory)
                        .Include(p => p.Product)
                            .ThenInclude(p => p.City)
                        .Include(p => p.Product)
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
                 .Take(pageSize).ToList();

            var result = list.Select(x => new ProductResponseDTO()
            {
                Id = x.ProductId,
                Title = x.Product.Title,
                Price = x.Product.Price,
                City = x.Product.City.Name,
                Currency = x.Product.Currency.Name,
                CurrencySymbol = x.Product.Currency.Symbol,
                DateTime = x.Product.DateTime,
                IsDisable = x.Product.IsDisable,
                Images = x.Product.Images.Select(i => i.Image).ToList(),
                UserType = x.Product.User.Type,
                Type = x.Product.Subcategory.Type
            }).ToList();

            foreach (var item in result)
            {
                item.IsFavorite = await CheckProductOnFavorite(item.Id, userId);
            }

            return new ProductsResponseDTO<ProductResponseDTO>(result, count, (int)Math.Ceiling(Convert.ToDouble(count) / pageSize), maxPrice, minPrice);
        }

        public async Task<bool> CheckProductOnFavorite(string productId, string userId)
        {
            if (userId == null) return false;
            
            var item = await _context.FavoriteProducts.Where(x => x.ProductId.Equals(productId) && x.UserId.Equals(userId)).FirstOrDefaultAsync();
            
            if (item == null) return false;
            
            return true;
        }

        public async Task RemoveFavoriteByProductId(string productId)
        {
            foreach (var product in await _context.FavoriteProducts.Where(f => f.ProductId.Equals(productId)).ToListAsync())
            {
                await DeleteAsync(product);
            }
        }
    }
}
