using Microsoft.EntityFrameworkCore;
using Swapy.Common.Entities;
using Swapy.Common.Exceptions;
using Swapy.DAL.Interfaces;

namespace Swapy.DAL.Repositories
{
    public class MemoryRepository : IMemoryRepository
    {
        private readonly SwapyDbContext _context;

        public MemoryRepository(SwapyDbContext context) => _context = context;

        public async Task CreateAsync(Memory item)
        {
            await _context.Memories.AddAsync(item);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(Memory item)
        {
            _context.Memories.Update(item);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(Memory item)
        {
            _context.Memories.Remove(item);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteByIdAsync(string id) => await DeleteAsync(await GetByIdAsync(id));

        public async Task<Memory> GetByIdAsync(string id)
        {
            var item = await _context.Memories.FindAsync(id);
            if (item == null) throw new NotFoundException($"{GetType().Name.Split("Repository")[0]} with {id} id not found");
            return item;
        }

        public async Task<IEnumerable<Memory>> GetAllAsync()
        {
            return await _context.Memories.ToListAsync();
        }

        public async Task<IEnumerable<Memory>> GetByModelAsync(string modelId)
        {
            return await _context.Memories.Include(x => x.MemoriesModels)
                .Where(x => modelId == null || x.MemoriesModels.Select(x => x.ModelId).Contains(modelId))
                .OrderBy(x => x.Name)
                .ToListAsync();
        }
    }
}
