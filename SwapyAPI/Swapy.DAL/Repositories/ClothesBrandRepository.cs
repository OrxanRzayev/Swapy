using Microsoft.EntityFrameworkCore;
using Swapy.Common.Entities;
using Swapy.Common.Exceptions;
using Swapy.DAL.Interfaces;
using System.Linq;

namespace Swapy.DAL.Repositories
{
    public class ClothesBrandRepository : IClothesBrandRepository
    {
        private readonly SwapyDbContext _context;

        public ClothesBrandRepository(SwapyDbContext context) => _context = context;

        public async Task CreateAsync(ClothesBrand item)
        {
            await _context.ClothesBrands.AddAsync(item);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(ClothesBrand item)
        {
            _context.ClothesBrands.Update(item);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(ClothesBrand item)
        {
            _context.ClothesBrands.Remove(item);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteByIdAsync(string id) => await DeleteAsync(await GetByIdAsync(id));

        public async Task<ClothesBrand> GetByIdAsync(string id)
        {
            var item = await _context.ClothesBrands.FindAsync(id);
            if (item == null) throw new NotFoundException($"{GetType().Name.Split("Repository")[0]} with {id} id not found");
            return item;
        }

        public async Task<IEnumerable<ClothesBrand>> GetAllAsync()
        {
            return await _context.ClothesBrands.ToListAsync();
        }

        public async Task<IEnumerable<ClothesBrand>> GetByClothesViewsAsync(IEnumerable<string> clothesViewsId)
        {
            var query = _context.ClothesBrands.Include(x => x.ClothesBrandsViews).AsQueryable();

            if (clothesViewsId != null)
            {
                query = query.Where(x => x.ClothesBrandsViews.Any(cbv => clothesViewsId.Contains(cbv.ClothesViewId)));
            }

            return await query.OrderBy(x => x.Name).ToListAsync();
        }
    }
}
