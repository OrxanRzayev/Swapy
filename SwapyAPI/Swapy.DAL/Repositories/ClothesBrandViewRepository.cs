using Microsoft.EntityFrameworkCore;
using Swapy.Common.Entities;
using Swapy.Common.Exceptions;
using Swapy.DAL.Interfaces;

namespace Swapy.DAL.Repositories
{
    public class ClothesBrandViewRepository : IClothesBrandViewRepository
    {
        private readonly SwapyDbContext _context;

        public ClothesBrandViewRepository(SwapyDbContext context) => _context = context;

        public async Task CreateAsync(ClothesBrandView item)
        {
            await _context.ClothesBrandsViews.AddAsync(item);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(ClothesBrandView item)
        {
            _context.ClothesBrandsViews.Update(item);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(ClothesBrandView item)
        {
            _context.ClothesBrandsViews.Remove(item);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteByIdAsync(string id) => await DeleteAsync(await GetByIdAsync(id));

        public async Task<ClothesBrandView> GetByIdAsync(string id)
        {
            var item = await _context.ClothesBrandsViews.FindAsync(id);
            if (item == null) throw new NotFoundException($"{GetType().Name.Split("Repository")[0]} with {id} id not found");
            return item;
        }

        public async Task<string> GetIdByBrandAndView(string brandId, string viewId)
        {
            var item = await _context.ClothesBrandsViews.Where(x => x.ClothesBrandId.Equals(brandId) && x.ClothesViewId.Equals(viewId)).FirstOrDefaultAsync();
            if (item == null) throw new NotFoundException($"{GetType().Name.Split("Repository")[0]} with brand id: {brandId} and view id: {viewId} not found");
            return item.Id;
        }

        public async Task<IEnumerable<ClothesBrandView>> GetAllAsync()
        {
            return await _context.ClothesBrandsViews.ToListAsync();
        }
    }
}
