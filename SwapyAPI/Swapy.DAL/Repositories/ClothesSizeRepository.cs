using Microsoft.EntityFrameworkCore;
using Swapy.Common.Entities;
using Swapy.Common.Exceptions;
using Swapy.DAL.Interfaces;

namespace Swapy.DAL.Repositories
{
    public class ClothesSizeRepository : IClothesSizeRepository
    {
        private readonly SwapyDbContext _context;

        public ClothesSizeRepository(SwapyDbContext context) => _context = context;

        public async Task CreateAsync(ClothesSize item)
        {
            await _context.ClothesSizes.AddAsync(item);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(ClothesSize item)
        {
            _context.ClothesSizes.Update(item);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(ClothesSize item)
        {
            _context.ClothesSizes.Remove(item);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteByIdAsync(string id) => await DeleteAsync(await GetByIdAsync(id));

        public async Task<ClothesSize> GetByIdAsync(string id)
        {
            var item = await _context.ClothesSizes.FindAsync(id);
            if (item == null) throw new NotFoundException($"{GetType().Name.Split("Repository")[0]} with {id} id not found");
            return item;
        }

        public async Task<IEnumerable<ClothesSize>> GetAllAsync()
        {
            return await _context.ClothesSizes.OrderBy(x => x.Size).ToListAsync();
        }

        public async Task<IEnumerable<ClothesSize>> GetByChildAndShoeAsync(bool isChild, bool isShoe)
        {
            var result = await _context.ClothesSizes
                .Where(cs => (cs.IsChild == isChild) && (cs.IsShoe == isShoe))
                .OrderBy(x => x.Size)
                .ToListAsync();

            return result;
        }
    }
}
