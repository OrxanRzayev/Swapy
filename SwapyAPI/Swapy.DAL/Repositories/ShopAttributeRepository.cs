using Microsoft.EntityFrameworkCore;
using Swapy.Common.DTO.Shops.Responses;
using Swapy.Common.Entities;
using Swapy.Common.Exceptions;
using Swapy.DAL.Interfaces;

namespace Swapy.DAL.Repositories
{
    public class ShopAttributeRepository : IShopAttributeRepository
    {
        private readonly SwapyDbContext _context;

        public ShopAttributeRepository(SwapyDbContext context) => _context = context;

        public async Task CreateAsync(ShopAttribute item)
        {
            await _context.ShopAttributes.AddAsync(item);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(ShopAttribute item)
        {
            _context.ShopAttributes.Update(item);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(ShopAttribute item)
        {
            _context.ShopAttributes.Remove(item);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteByIdAsync(string id) => await DeleteAsync(await GetByIdAsync(id));

        public async Task<ShopAttribute> GetByIdAsync(string id)
        {
            var item = await _context.ShopAttributes.FindAsync(id);
            if (item == null) throw new NotFoundException($"{GetType().Name.Split("Repository")[0]} with {id} id not found");
            return item;
        }
        
        public async Task<ShopAttribute> GetByUserIdAsync(string userId)
        {
            var item = await _context.ShopAttributes.Include(s => s.User).FirstOrDefaultAsync(s => s.UserId.Equals(userId));
            if (item == null) throw new NotFoundException($"{GetType().Name.Split("Repository")[0]} with {userId} id not found");
            if (item.User.EmailConfirmed == false) throw new NotFoundException($"{GetType().Name.Split("Repository")[0]} with {userId} id not found");
            return item;
        }        

        public async Task<ShopAttribute> GetDetailByIdAsync(string id)
        {
            var item = await _context.ShopAttributes.Include(s => s.User).FirstOrDefaultAsync(s => s.Id.Equals(id));
            if (item == null) throw new NotFoundException($"{GetType().Name.Split("Repository")[0]} with {id} id not found");
            if (item.User.EmailConfirmed == false) throw new NotFoundException($"{GetType().Name.Split("Repository")[0]} with {id} id not found");
            return item;
        }

        public async Task<IEnumerable<ShopAttribute>> GetAllAsync()
        {
            return await _context.ShopAttributes.ToListAsync();
        }

        public async Task<ShopsResponseDTO> GetAllFilteredAsync(int page, int pageSize, string title, bool? sortByViews, bool? reverseSort)
        {
            if (page < 1 || pageSize < 1) throw new ArgumentException($"Page and page size parameters must be greater than one.");

            var query = _context.ShopAttributes.Where(s => title == null || s.ShopName.Contains(title))
                                               .Include(s => s.User)
                                                .ThenInclude(u => u.Products)
                                               .Where(s => s.User.EmailConfirmed == true)
                                               .AsQueryable();

            var count = await query.CountAsync();
            if (count <= pageSize * (page - 1)) throw new NotFoundException($"Page {page} not found.");

            if (sortByViews == true) query = query.OrderBy(s => s.Views);
            else query = query.OrderBy(s => s.ShopName);
            if (reverseSort == true) query = query.Reverse();

            query = query.Skip(pageSize * (page - 1))
                 .Take(pageSize);

            var result = await query.Select(x => new ShopResponseDTO
            {
                ShopId = x.Id,
                UserId = x.UserId,
                Logo = x.User.Logo,
                ShopName = x.ShopName,
                Description = x.Description,
                PhoneNumber = x.User.PhoneNumber,
                ProductCount = x.User.ProductsCount
            }).ToListAsync();

            return new ShopsResponseDTO(result, count, (int)Math.Ceiling(Convert.ToDouble(count) / pageSize));
        }

        public async Task<bool> FindByShopNameAsync(string shopName)
        {
            var result = await _context.ShopAttributes.FirstOrDefaultAsync(s => s.ShopName.Equals(shopName));
            if(result == null) return false;
            return true;
        }

        public async Task IncrementViewsAsync(string shopId)
        {
            var shop = await GetByIdAsync(shopId);
            shop.Views++;
            await UpdateAsync(shop);
        }
    }
}
