using Microsoft.EntityFrameworkCore;
using Swapy.Common.Entities;
using Swapy.Common.Exceptions;
using Swapy.DAL.Interfaces;

namespace Swapy.DAL.Repositories
{
    public class ElectronicBrandTypeRepository : IElectronicBrandTypeRepository
    {
        private readonly SwapyDbContext _context;

        public ElectronicBrandTypeRepository(SwapyDbContext context) => _context = context;

        public async Task CreateAsync(ElectronicBrandType item)
        {
            await _context.ElectronicBrandsTypes.AddAsync(item);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(ElectronicBrandType item)
        {
            _context.ElectronicBrandsTypes.Update(item);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(ElectronicBrandType item)
        {
            _context.ElectronicBrandsTypes.Remove(item);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteByIdAsync(string id) => await DeleteAsync(await GetByIdAsync(id));

        public async Task<ElectronicBrandType> GetByIdAsync(string id)
        {
            var item = await _context.ElectronicBrandsTypes.FindAsync(id);
            if (item == null) throw new NotFoundException($"{GetType().Name.Split("Repository")[0]} with {id} id not found");
            return item;
        }

        public async Task<IEnumerable<ElectronicBrandType>> GetAllAsync()
        {
            return await _context.ElectronicBrandsTypes.ToListAsync();
        }
    }
}
