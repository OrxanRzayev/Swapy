using Microsoft.EntityFrameworkCore;
using Swapy.Common.DTO.Products.Responses;
using Swapy.Common.Entities;
using Swapy.Common.Exceptions;
using Swapy.DAL.Interfaces;

namespace Swapy.DAL.Repositories
{
    public class ColorRepository : IColorRepository
    {
        private readonly SwapyDbContext _context;

        public ColorRepository(SwapyDbContext context) => _context = context;

        public async Task CreateAsync(Color item)
        {
            await _context.Colors.AddAsync(item);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(Color item)
        {
            _context.Colors.Update(item);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(Color item)
        {
            _context.Colors.Remove(item);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteByIdAsync(string id) => await DeleteAsync(await GetByIdAsync(id));

        public async Task<Color> GetByIdAsync(string id)
        {
            var item = await _context.Colors.FindAsync(id);
            if (item == null) throw new NotFoundException($"{GetType().Name.Split("Repository")[0]} with {id} id not found");
            return item;
        }

        public async Task<IEnumerable<Color>> GetAllAsync()
        {
            return await _context.Colors.ToListAsync();
        }

        public async Task<IEnumerable<SpecificationResponseDTO<string>>> GetAllSpecificationAsync()
        {
            return _context.Colors.AsEnumerable()
                                  .Select(s => new SpecificationResponseDTO<string>(s.Id, s.Name))
                                  .OrderBy(s => s.Value)
                                  .ToList();
        }

        public async Task<IEnumerable<SpecificationResponseDTO<string>>> GetByModelAsync(string modelId)
        {
            return _context.Colors.Include(x => x.ModelsColors)
                                  .Where(x => modelId == null || x.ModelsColors.Select(x => x.ModelId).Contains(modelId))
                                  .AsEnumerable()
                                  .Select(s => new SpecificationResponseDTO<string>(s.Id, s.Name))
                                  .OrderBy(s => s.Value)
                                  .ToList();
        }
    }
}

