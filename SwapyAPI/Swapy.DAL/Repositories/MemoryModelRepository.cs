using Microsoft.EntityFrameworkCore;
using Swapy.Common.DTO.Electronics.Requests.Queries;
using Swapy.Common.Entities;
using Swapy.Common.Exceptions;
using Swapy.DAL.Interfaces;

namespace Swapy.DAL.Repositories
{
    public class MemoryModelRepository : IMemoryModelRepository
    {
        private readonly SwapyDbContext _context;

        public MemoryModelRepository(SwapyDbContext context) => _context = context;

        public async Task CreateAsync(MemoryModel item)
        {
            await _context.MemoriesModels.AddAsync(item);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(MemoryModel item)
        {
            _context.MemoriesModels.Update(item);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(MemoryModel item)
        {
            _context.MemoriesModels.Remove(item);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteByIdAsync(string id) => await DeleteAsync(await GetByIdAsync(id));

        public async Task<string> GetByMemoryAndModelAsync(string memoryId, string modelId)
        {
            var item = await _context.MemoriesModels.Where(x => x.MemoryId.Equals(memoryId) && x.ModelId.Equals(modelId)).FirstOrDefaultAsync();
            if (item == null) throw new NotFoundException($"{GetType().Name.Split("Repository")[0]} with memory id: {memoryId} and model id: {modelId} id not found");
            return item.Id;
        }

        public async Task<MemoryModel> GetByIdAsync(string id)
        {
            var item = await _context.MemoriesModels.FindAsync(id);
            if (item == null) throw new NotFoundException($"{GetType().Name.Split("Repository")[0]} with {id} id not found");
            return item;
        }

        public async Task<IEnumerable<MemoryModel>> GetAllAsync()
        {
            return await _context.MemoriesModels.ToListAsync();
        }
    }
}
