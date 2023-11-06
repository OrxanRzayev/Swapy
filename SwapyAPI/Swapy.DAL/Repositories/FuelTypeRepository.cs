using Microsoft.EntityFrameworkCore;
using Swapy.Common.DTO.Products.Responses;
using Swapy.Common.Entities;
using Swapy.Common.Exceptions;
using Swapy.DAL.Interfaces;

namespace Swapy.DAL.Repositories
{
    public class FuelTypeRepository : IFuelTypeRepository
    {
        private readonly SwapyDbContext _context;

        public FuelTypeRepository(SwapyDbContext context) => _context = context;

        public async Task CreateAsync(FuelType item)
        {
            await _context.FuelTypes.AddAsync(item);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(FuelType item)
        {
            _context.FuelTypes.Update(item);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(FuelType item)
        {
            _context.FuelTypes.Remove(item);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteByIdAsync(string id) => await DeleteAsync(await GetByIdAsync(id));

        public async Task<FuelType> GetByIdAsync(string id)
        {
            var item = await _context.FuelTypes.FindAsync(id);
            if (item == null) throw new NotFoundException($"{GetType().Name.Split("Repository")[0]} with {id} id not found");
            return item;
        }

        public async Task<IEnumerable<FuelType>> GetAllAsync()
        {
            return await _context.FuelTypes.ToListAsync();
        }

        public async Task<IEnumerable<SpecificationResponseDTO<string>>> GetAllSpecificationAsync()
        {
            return _context.FuelTypes.AsEnumerable()
                                     .Select(s => new SpecificationResponseDTO<string>(s.Id, s.Name))
                                     .OrderBy(s => s.Value)
                                     .ToList();
        }
    }
}
