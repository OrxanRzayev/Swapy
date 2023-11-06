using Microsoft.EntityFrameworkCore;
using Swapy.Common.Entities;
using Swapy.Common.Exceptions;
using Swapy.DAL.Interfaces;

namespace Swapy.DAL.Repositories
{
    public class AutoBrandRepository : IAutoBrandRepository
    {
        private readonly SwapyDbContext _context;

        public AutoBrandRepository(SwapyDbContext context) => _context = context;

        public async Task CreateAsync(AutoBrand item)
        {
            await _context.AutoBrands.AddAsync(item);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(AutoBrand item)
        {
            _context.AutoBrands.Update(item);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(AutoBrand item)
        {
            _context.AutoBrands.Remove(item);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteByIdAsync(string id) => await DeleteAsync(await GetByIdAsync(id));

        public async Task<AutoBrand> GetByIdAsync(string id)
        {
            var item = await _context.AutoBrands.FindAsync(id);
            if (item == null) throw new NotFoundException($"{GetType().Name.Split("Repository")[0]} with {id} id not found");
            return item;
        }

        public async Task<IEnumerable<AutoBrand>> GetAllAsync()
        {
            return await _context.AutoBrands.ToListAsync();
        }

        public async Task<IEnumerable<AutoBrand>> GetByAutoTypesAsync(IEnumerable<string> autoTypesId)
        {
            var query = _context.AutoBrands.Include(x => x.AutoModels).AsQueryable();

            if (autoTypesId != null && autoTypesId.Any())
            {
                query = query.Where(x => x.AutoModels.Any(model => autoTypesId.Contains(model.AutoTypeId)));
            }

            return await query.OrderBy(x => x.Name).ToListAsync();
        }
    }
}

