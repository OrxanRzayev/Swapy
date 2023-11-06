using Microsoft.EntityFrameworkCore;
using Swapy.Common.DTO.Products.Responses;
using Swapy.Common.Entities;
using Swapy.Common.Exceptions;
using Swapy.DAL.Interfaces;

namespace Swapy.DAL.Repositories
{
    public class TVTypeRepository : ITVTypeRepository
    {
        private readonly SwapyDbContext _context;

        public TVTypeRepository(SwapyDbContext context) => _context = context;

        public async Task CreateAsync(TVType item)
        {
            await _context.TVTypes.AddAsync(item);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(TVType item)
        {
            _context.TVTypes.Update(item);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(TVType item)
        {
            _context.TVTypes.Remove(item);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteByIdAsync(string id) => await DeleteAsync(await GetByIdAsync(id));

        public async Task<TVType> GetByIdAsync(string id)
        {
            var item = await _context.TVTypes.FindAsync(id);
            if (item == null) throw new NotFoundException($"{GetType().Name.Split("Repository")[0]} with {id} id not found");
            return item;
        }

        public async Task<IEnumerable<TVType>> GetAllAsync()
        {
            return await _context.TVTypes.ToListAsync();
        }

        public async Task<IEnumerable<SpecificationResponseDTO<string>>> GetAllSpecificationAsync()
        {
            return _context.TVTypes.AsEnumerable()
                                   .Select(s => new SpecificationResponseDTO<string>(s.Id, s.Name))
                                   .OrderBy(s => s.Value)
                                   .ToList();
        }
    }
}
