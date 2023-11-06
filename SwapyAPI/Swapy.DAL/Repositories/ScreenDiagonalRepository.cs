using Microsoft.EntityFrameworkCore;
using Swapy.Common.Entities;
using Swapy.Common.Exceptions;
using Swapy.DAL.Interfaces;

namespace Swapy.DAL.Repositories
{
    public class ScreenDiagonalRepository : IScreenDiagonalRepository
    {
        private readonly SwapyDbContext _context;

        public ScreenDiagonalRepository(SwapyDbContext context) => _context = context;

        public async Task CreateAsync(ScreenDiagonal item)
        {
            await _context.ScreenDiagonals.AddAsync(item);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(ScreenDiagonal item)
        {
            _context.ScreenDiagonals.Update(item);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(ScreenDiagonal item)
        {
            _context.ScreenDiagonals.Remove(item);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteByIdAsync(string id) => await DeleteAsync(await GetByIdAsync(id));

        public async Task<ScreenDiagonal> GetByIdAsync(string id)
        {
            var item = await _context.ScreenDiagonals.FindAsync(id);
            if (item == null) throw new NotFoundException($"{GetType().Name.Split("Repository")[0]} with {id} id not found");
            return item;
        }

        public async Task<IEnumerable<ScreenDiagonal>> GetAllAsync()
        {
            return await _context.ScreenDiagonals.OrderBy(x => x.Diagonal).ToListAsync();
        }
    }
}
