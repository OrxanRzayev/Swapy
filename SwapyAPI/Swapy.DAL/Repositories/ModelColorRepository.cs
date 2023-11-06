using Microsoft.EntityFrameworkCore;
using Swapy.Common.Entities;
using Swapy.Common.Exceptions;
using Swapy.DAL.Interfaces;

namespace Swapy.DAL.Repositories
{
    public class ModelColorRepository : IModelColorRepository
    {
        private readonly SwapyDbContext _context;

        public ModelColorRepository(SwapyDbContext context) => _context = context;

        public async Task CreateAsync(ModelColor item)
        {
            await _context.ModelsColors.AddAsync(item);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(ModelColor item)
        {
            _context.ModelsColors.Update(item);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(ModelColor item)
        {
            _context.ModelsColors.Remove(item);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteByIdAsync(string id) => await DeleteAsync(await GetByIdAsync(id));

        public async Task<string> GetByModelAndColorAsync(string modelId, string colorId)
        {
            var item = await _context.ModelsColors.Where(x => x.ModelId.Equals(modelId) && x.ColorId.Equals(colorId)).FirstOrDefaultAsync();
            if (item == null) throw new NotFoundException($"{GetType().Name.Split("Repository")[0]} with model id: {modelId} and color id: {colorId} not found");
            return item.Id;
        }

        public async Task<ModelColor> GetByIdAsync(string id)
        {
            var item = await _context.ModelsColors.FindAsync(id);
            if (item == null) throw new NotFoundException($"{GetType().Name.Split("Repository")[0]} with {id} id not found");
            return item;
        }

        public async Task<IEnumerable<ModelColor>> GetAllAsync()
        {
            return await _context.ModelsColors.ToListAsync();
        }
    }
}
