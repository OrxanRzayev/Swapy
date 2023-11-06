using Microsoft.EntityFrameworkCore;
using Swapy.DAL.Interfaces;
using Swapy.Common.Entities;
using Swapy.Common.Exceptions;

namespace Swapy.DAL.Repositories
{
    public class AutoModelRepository : IAutoModelRepository
    {
        private readonly SwapyDbContext _context;

        public AutoModelRepository(SwapyDbContext context) => _context = context;

        public async Task CreateAsync(AutoModel item)
        {
            await _context.AutoBrandsTypes.AddAsync(item);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(AutoModel item)
        {
            _context.AutoBrandsTypes.Update(item);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(AutoModel item)
        {
            _context.AutoBrandsTypes.Remove(item);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteByIdAsync(string id) => await DeleteAsync(await GetByIdAsync(id));

        public async Task<AutoModel> GetByIdAsync(string id)
        {
            var item = await _context.AutoBrandsTypes.FindAsync(id);
            if (item == null) throw new NotFoundException($"{GetType().Name.Split("Repository")[0]} with {id} id not found");
            return item;
        }

        public async Task<IEnumerable<AutoModel>> GetAllAsync()
        {
            return await _context.AutoBrandsTypes.ToListAsync();
        }

        public async Task<IEnumerable<AutoModel>> GetByBrandsAndTypesAsync(IEnumerable<string> autoBrandsId, IEnumerable<string> autoTypesId)
        {
            return await _context.AutoBrandsTypes.Where(am =>
                (autoBrandsId == null || autoBrandsId.Contains(am.AutoBrandId)) &&
                (autoTypesId == null || autoTypesId.Contains(am.AutoTypeId)))
                .OrderBy(am => am.Name)
                .ToListAsync();
        }
    }
}
