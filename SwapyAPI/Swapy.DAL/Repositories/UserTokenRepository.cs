using Microsoft.EntityFrameworkCore;
using Swapy.Common.Entities;
using Swapy.Common.Exceptions;
using Swapy.DAL.Interfaces;

namespace Swapy.DAL.Repositories
{
    public class UserTokenRepository : IUserTokenRepository
    {
        private readonly SwapyDbContext _context;

        public UserTokenRepository(SwapyDbContext context) => _context = context;

        public async Task CreateAsync(UserToken item)
        {
            await _context.UserTokens.AddAsync(item);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(UserToken item)
        {
            _context.UserTokens.Update(item);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(UserToken item)
        {
            _context.UserTokens.Remove(item);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteByIdAsync(string id) => await DeleteAsync(await GetByIdAsync(id));
        
        public async Task<UserToken> GetByIdAsync(string id)
        {
            var item = await _context.UserTokens.FindAsync(id);
            if (item == null) throw new NotFoundException($"{GetType().Name.Split("Repository")[0]} with {id} id not found");
            return item;
        }

        public async Task<IEnumerable<UserToken>> GetAllAsync()
        {
            return await _context.UserTokens.ToListAsync();
        }

        public async Task<UserToken> GetByAccessTokenAsync(string accessToken)
        {
            return await _context.UserTokens.Include(ut => ut.User).FirstOrDefaultAsync(ut => ut.AccessToken.Equals(accessToken));
        }
    }
}
