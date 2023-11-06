using Microsoft.EntityFrameworkCore;
using Swapy.Common.Entities;
using Swapy.Common.Exceptions;
using Swapy.DAL.Interfaces;

namespace Swapy.DAL.Repositories
{ 
    public class ScreenResolutionRepository : IScreenResolutionRepository
    {
        private readonly SwapyDbContext _context;

        public ScreenResolutionRepository(SwapyDbContext context) => _context = context;

        public async Task CreateAsync(ScreenResolution item)
        {
            await _context.ScreenResolutions.AddAsync(item);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(ScreenResolution item)
        {
            _context.ScreenResolutions.Update(item);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(ScreenResolution item)
        {
            _context.ScreenResolutions.Remove(item);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteByIdAsync(string id) => await DeleteAsync(await GetByIdAsync(id));

        public async Task<ScreenResolution> GetByIdAsync(string id)
        {
            var item = await _context.ScreenResolutions.FindAsync(id);
            if (item == null) throw new NotFoundException($"{GetType().Name.Split("Repository")[0]} with {id} id not found");
            return item;
        }

        public async Task<IEnumerable<ScreenResolution>> GetAllAsync()
        {
            return await _context.ScreenResolutions.OrderBy(x => x.Name).ToListAsync();
        }
    }
}
