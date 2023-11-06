using Swapy.Common.Entities;

namespace Swapy.DAL.Interfaces
{
    public interface IUserLikeRepository : IRepository<UserLike>
    {
        Task<IEnumerable<UserLike>> GetAllByUserIdAsync(string userId);
        Task<int> GetCountByUserIdAsync(string userId);
        Task<bool> CheckUserLikeAsync(string likerId, string recipientId);
        Task<UserLike> GetUserLikeByRecipientAsync(string likerId, string recipientId);
    }
}
