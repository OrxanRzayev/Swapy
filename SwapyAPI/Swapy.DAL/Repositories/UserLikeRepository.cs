using Microsoft.EntityFrameworkCore;
using Swapy.Common.Entities;
using Swapy.Common.Exceptions;
using Swapy.DAL.Interfaces;

namespace Swapy.DAL.Repositories
{
    public class UserLikeRepository : IUserLikeRepository
    {
        private readonly SwapyDbContext _context;

        public UserLikeRepository(SwapyDbContext context) => _context = context;

        public async Task CreateAsync(UserLike item)
        {
            await _context.UsersLikes.AddAsync(item);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(UserLike item)
        {
            _context.UsersLikes.Update(item);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(UserLike item)
        {
            _context.UsersLikes.Remove(item);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteByIdAsync(string id) => await DeleteAsync(await GetByIdAsync(id));

        public async Task<UserLike> GetByIdAsync(string id)
        {
            var item = await _context.UsersLikes.FindAsync(id);
            if (item == null) throw new NotFoundException($"{GetType().Name.Split("Repository")[0]} with {id} id not found");
            return item;
        }

        public async Task<IEnumerable<UserLike>> GetAllAsync()
        {
            return await _context.UsersLikes.ToListAsync();
        }

        public async Task<IEnumerable<UserLike>> GetAllByUserIdAsync(string userId)
        {
            return await _context.UsersLikes.Where(ul => ul.RecipientId.Equals(userId)).ToListAsync();
        }

        public async Task<int> GetCountByUserIdAsync(string userId)
        {
            return await _context.UsersLikes.CountAsync(ul => ul.RecipientId.Equals(userId));
        }

        public async Task<bool> CheckUserLikeAsync(string likerId, string recipientId)
        {
            var item = await _context.UsersLikes.Include(ul => ul.Like)
                                                .FirstOrDefaultAsync(ul => ul.RecipientId.Equals(recipientId) && ul.Like.LikerId.Equals(likerId));

            if (item == null) return false;
            
            return true;
        }

        public async Task<UserLike> GetUserLikeByRecipientAsync(string likerId, string recipientId)
        {
            var item = await _context.UsersLikes.Include(ul => ul.Like).FirstOrDefaultAsync(ul => ul.RecipientId.Equals(recipientId) && ul.Like.LikerId.Equals(likerId));
            if (item == null) throw new NotFoundException($"{GetType().Name.Split("Repository")[0]} with {likerId} id not found");
            return item;
        }
    }
}
