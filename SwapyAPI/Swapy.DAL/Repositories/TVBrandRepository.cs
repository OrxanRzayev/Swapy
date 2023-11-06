using Microsoft.EntityFrameworkCore;
using Swapy.Common.Entities;
using Swapy.Common.Exceptions;
using Swapy.DAL.Interfaces;

namespace Swapy.DAL.Repositories
{
    public class TVBrandRepository : ITVBrandRepository
    {
        private readonly SwapyDbContext _context;

        public TVBrandRepository(SwapyDbContext context) => _context = context;

        public async Task CreateAsync(TVBrand item)
        {
            await _context.TVBrands.AddAsync(item);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(TVBrand item)
        {
            _context.TVBrands.Update(item);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(TVBrand item)
        {
            _context.TVBrands.Remove(item);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteByIdAsync(string id) => await DeleteAsync(await GetByIdAsync(id));

        public async Task<TVBrand> GetByIdAsync(string id)
        {
            var item = await _context.TVBrands.FindAsync(id);
            if (item == null) throw new NotFoundException($"{GetType().Name.Split("Repository")[0]} with {id} id not found");
            return item;
        }

        public async Task<IEnumerable<TVBrand>> GetAllAsync()
        {
            return await _context.TVBrands.OrderBy(x => x.Name).ToListAsync();
        }
    }
}
