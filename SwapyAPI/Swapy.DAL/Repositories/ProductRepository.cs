using Microsoft.EntityFrameworkCore;
using Swapy.BLL.Interfaces;
using Swapy.Common.DTO.Products.Responses;
using Swapy.Common.Entities;
using Swapy.Common.Enums;
using Swapy.Common.Exceptions;
using Swapy.DAL.Interfaces;
using System.Linq;

namespace Swapy.DAL.Repositories
{
    public class ProductRepository : IProductRepository
    {
        private readonly SwapyDbContext _context;

        private readonly IFavoriteProductRepository _favoriteProductRepository;

        private readonly ISubcategoryRepository _subcategoryRepository;

        private readonly ICurrencyRepository _currencyRepository;

        private readonly ICurrencyConverterService _currencyConverterService;

        public ProductRepository(SwapyDbContext context, IFavoriteProductRepository favoriteProductRepository, ISubcategoryRepository subcategoryRepository, ICurrencyRepository currencyRepository, ICurrencyConverterService currencyConverterService)
        {
            _context = context;
            _favoriteProductRepository = favoriteProductRepository;
            _subcategoryRepository = subcategoryRepository;
            _currencyConverterService = currencyConverterService;
            _currencyRepository = currencyRepository;
        }

        public async Task CreateAsync(Product item)
        {
            await _context.Products.AddAsync(item);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(Product item)
        {
            _context.Products.Update(item);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(Product item)
        {
            _context.Products.Remove(item);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteByIdAsync(string id) => await DeleteAsync(await GetByIdAsync(id));

        public async Task<Product> GetByIdAsync(string id)
        {
            var item = await _context.Products.FindAsync(id);
            if (item == null) throw new NotFoundException($"{GetType().Name.Split("Repository")[0]} with {id} id not found");
            return item;
        }

        public async Task<IEnumerable<Product>> GetAllAsync()
        {
            return await _context.Products.ToListAsync();
        }

        public async Task<IEnumerable<Product>> GetAllByUserId(string userId)
        {
            return await _context.Products.Where(p => p.UserId.Equals(userId)).ToListAsync();
        }

        public async Task<Product> GetDetailByIdAsync(string id)
        {
            var item = await _context.Products.Where(a => a.Id.Equals(id))
                                              .Include(p => p.Images)
                                              .Include(p => p.City)
                                              .Include(p => p.Currency)
                                              .Include(p => p.Subcategory)
                                              .FirstOrDefaultAsync();

            if (item == null) throw new NotFoundException($"{GetType().Name.Split("Repository")[0]} with {id} id not found");
            return item;
        }

        public async Task<SpecificationResponseDTO<CategoryType>> GetProductCategoryTypeAsync(string id)
        {
            var item = await _context.Products.Where(a => a.Id.Equals(id))
                                    .Include(p => p.Subcategory)
                                    .Select(p => new SpecificationResponseDTO<CategoryType>(p.CategoryId, p.Subcategory.Type))
                                    .FirstOrDefaultAsync();

            if (item == null) throw new NotFoundException($"no category found for {GetType().Name.Split("Repository")[0]} with {id} id");
            return item;
        }

        public async Task<ProductSubcategoryResponseDTO> GetProductSubcategoryAsync(string id)
        {
            var item = await _context.Products.Where(a => a.Id.Equals(id))
                                    .Include(p => p.Subcategory)
                                    .Select(p => new ProductSubcategoryResponseDTO(p.Subcategory.Id, p.Subcategory.Name, p.Subcategory.Type, p.CategoryId, p.Subcategory.SubType))
                                    .FirstOrDefaultAsync();

            if (item == null) throw new NotFoundException($"no category found for {GetType().Name.Split("Repository")[0]} with {id} id");
            return item;
        }

        public async Task IncrementViewsAsync(string id)
        {
            var item = await GetByIdAsync(id);
            item.Views++;
            await UpdateAsync(item);
        }

        public async Task<int> GetProductCountForShopAsync(string userId)
        {
            return await _context.Products.CountAsync(p => p.UserId.Equals(userId));
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
                                                                                       bool? isDisable,
                                                                                       bool? sortByPrice,
                                                                                       bool? reverseSort)
        {
            if (page < 1 || pageSize < 1) throw new InvalidPageNumberException($"Page and page size parameters must be greater than one.");

            bool isDisableResult = isDisable ?? false;

            if (isDisableResult && (string.IsNullOrEmpty(userId) && string.IsNullOrEmpty(otherUserId))) throw new NoAccessException("No access to get disabled products");

            List<SpecificationResponseDTO<string>> sequenceOfSubcategories = subcategoryId == null ? new() : (await _subcategoryRepository.GetAllChildsOfSubcategory(subcategoryId)).ToList();

            var query = _context.Products.Include(p => p.Currency)
                                         .AsQueryable();

            decimal? minPrice = currencyId == null ? null : Math.Floor((await query.ToListAsync()).Select(x => _currencyConverterService.Convert(x.Currency.Name, _currencyRepository.GetById(currencyId).Name, x.Price)).OrderBy(p => p).FirstOrDefault());
            decimal? maxPrice = currencyId == null ? null : Math.Ceiling((await query.ToListAsync()).Select(x => _currencyConverterService.Convert(x.Currency.Name, _currencyRepository.GetById(currencyId).Name, x.Price)).OrderBy(p => p).LastOrDefault());

            var list = await query.Where(x => (title == null || x.Title.Contains(title)) &&
                                     (categoryId == null || x.CategoryId.Equals(categoryId)) &&
                                     (subcategoryId == null ? true : sequenceOfSubcategories.Select(x => x.Id).Contains(x.SubcategoryId)) &&
                                     (cityId == null || x.CityId.Equals(cityId)) &&
                                     (otherUserId == null ? !x.UserId.Equals(userId) : x.UserId.Equals(otherUserId)) &&
                                     (isDisable == null || !x.UserId.Equals(userId) ? x.IsDisable == false : x.IsDisable.Equals(isDisable)))
                                    .Include(p => p.Images)
                                    .Include(p => p.Subcategory)
                                    .Include(p => p.City)
                                    .Include(p => p.User)
                                    .ToListAsync();

            list = list.Where(x => (priceMin == null || currencyId == null || priceMin <= _currencyConverterService.Convert(x.Currency.Name, _currencyRepository.GetById(currencyId).Name, x.Price)) &&
                        (priceMax == null || currencyId == null || priceMax >= _currencyConverterService.Convert(x.Currency.Name, _currencyRepository.GetById(currencyId).Name, x.Price))).ToList();

            var count = list.Count;
            if (count <= pageSize * (page - 1)) throw new NotFoundException($"Page {page} not found.");

            if (sortByPrice == true) list = list.OrderBy(x => _currencyConverterService.Convert(x.Currency.Name, "usd", x.Price)).ToList();
            else list = list.OrderBy(x => x.DateTime).ToList();
            if (reverseSort == true) list.Reverse();

            list = list.Skip(pageSize * (page - 1))
                 .Take(pageSize).ToList();

            var result = list.Select(x => new ProductResponseDTO()
            {
                Id = x.Id,
                Title = x.Title,
                Price = x.Price,
                City = x.City.Name,
                Currency = x.Currency.Name,
                CurrencySymbol = x.Currency.Symbol,
                DateTime = x.DateTime,
                Images = x.Images.Select(i => i.Image).ToList(),
                UserType = x.User.Type,
                UserId = x.UserId,
                IsDisable = x.IsDisable,
                Type = x.Subcategory.Type
            }).ToList();

            foreach (var item in result)
            {
                item.IsFavorite = userId == null ? false : await _favoriteProductRepository.CheckProductOnFavorite(item.Id, userId);
            }

            return new ProductsResponseDTO<ProductResponseDTO>(result, count, (int)Math.Ceiling(Convert.ToDouble(count) / pageSize), maxPrice, minPrice);
        }


        public async Task<ProductsResponseDTO<ProductResponseDTO>> GetSimilarProductsById(int page,
                                                                               int pageSize,
                                                                               string productId,
                                                                               string userId,
                                                                               string title,
                                                                               string currencyId,
                                                                               decimal? priceMin,
                                                                               decimal? priceMax,
                                                                               string categoryId,
                                                                               string subcategoryId,
                                                                               string cityId)
        {
            if (page < 1 || pageSize < 1) throw new InvalidPageNumberException($"Page and page size parameters must be greater than one.");

            var currentProduct = await _context.Products.Where(p => p.Id.Equals(productId)).FirstOrDefaultAsync();

            if (currentProduct == null) throw new ArgumentException($"{GetType().Name.Split("Repository")[0]} with {productId} id not found");

            var query = _context.Products.Where(x => !x.Id.Equals(productId))
                                         .Include(p => p.Currency)
                                         .AsQueryable();

            decimal? minPrice = currencyId == null ? null : Math.Floor((await query.ToListAsync()).Select(x => _currencyConverterService.Convert(x.Currency.Name, _currencyRepository.GetById(currencyId).Name, x.Price)).OrderBy(p => p).FirstOrDefault());
            decimal? maxPrice = currencyId == null ? null : Math.Ceiling((await query.ToListAsync()).Select(x => _currencyConverterService.Convert(x.Currency.Name, _currencyRepository.GetById(currencyId).Name, x.Price)).OrderBy(p => p).LastOrDefault());

            var list = await query.Where(x => (title == null || x.Title.Contains(title)) &&
                                               (categoryId == null || x.CategoryId.Equals(categoryId)) &&
                                               (subcategoryId == null || x.SubcategoryId.Equals(subcategoryId)) &&
                                               (cityId == null || x.CityId.Equals(cityId)) &&
                                               (userId == null || !x.UserId.Equals(userId)) &&
                                               x.IsDisable.Equals(false))
                                         .Include(p => p.Images)
                                         .Include(p => p.Subcategory)
                                         .Include(p => p.City)
                                         .Include(p => p.User)
                                         .ToListAsync();

            list = list.Where(x => (priceMin == null || currencyId == null || priceMin <= _currencyConverterService.Convert(x.Currency.Name, _currencyRepository.GetById(currencyId).Name, x.Price)) &&
                        (priceMax == null || currencyId == null || priceMax >= _currencyConverterService.Convert(x.Currency.Name, _currencyRepository.GetById(currencyId).Name, x.Price))).ToList();

            var count = list.Count;
            if (count <= pageSize * (page - 1)) throw new NotFoundException($"Page {page} not found.");

            var result = list.OrderBy(p => ComputeLevenshteinDistance(p.Title.ToLower(), currentProduct.Title.ToLower()))
                                     .ThenBy(p => (p.Description != null && currentProduct.Description != null) ? ComputeLevenshteinDistance(p.Description.ToLower(), currentProduct.Description.ToLower()) : int.MaxValue)
                                     .Skip(pageSize * (page - 1))
                                     .Take(pageSize)
                                     .Select(x => new ProductResponseDTO()
                                     {
                                         Id = x.Id,
                                         Title = x.Title,
                                         Price = x.Price,
                                         City = x.City.Name,
                                         Currency = x.Currency.Name,
                                         CurrencySymbol = x.Currency.Symbol,
                                         DateTime = x.DateTime,
                                         Images = x.Images.Select(i => i.Image).ToList(),
                                         UserType = x.User.Type,
                                         IsDisable = x.IsDisable,
                                         Type = x.Subcategory.Type
                                     });

            foreach (var item in result)
            {
                item.IsFavorite = userId == null ? false : await _favoriteProductRepository.CheckProductOnFavorite(item.Id, userId);
            }

            return new ProductsResponseDTO<ProductResponseDTO>(result, count, (int)Math.Ceiling(Convert.ToDouble(count) / pageSize), maxPrice, minPrice);
        }

        private int ComputeLevenshteinDistance(string s, string t)
        {
            int n = s.Length;
            int m = t.Length;
            int[,] d = new int[n + 1, m + 1];

            if (n == 0) return m;
            if (m == 0) return n;

            for (int i = 0; i <= n; d[i, 0] = i++) ;
            for (int j = 0; j <= m; d[0, j] = j++) ;

            for (int i = 1; i <= n; i++)
            {
                for (int j = 1; j <= m; j++)
                {
                    int cost = (t[j - 1] == s[i - 1]) ? 0 : 1;
                    d[i, j] = Math.Min(Math.Min(d[i - 1, j] + 1, d[i, j - 1] + 1), d[i - 1, j - 1] + cost);
                }
            }

            var test = d[n, m];

            return d[n, m];
        }
    }
}