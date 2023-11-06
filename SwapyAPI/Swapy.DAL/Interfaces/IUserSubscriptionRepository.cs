using Swapy.Common.Entities;

namespace Swapy.DAL.Interfaces
{
    public interface IUserSubscriptionRepository : IRepository<UserSubscription>
    {
        Task<int> GetCountByUserIdAsync(string userId);
        Task<bool> CheckUserSubscriptionAsync(string subscriberId, string recipientId);
        Task<UserSubscription> GetUserSubscriptionByRecipientAsync(string subscriberId, string recipientId);
        Task<List<User>> GetUserSubscriptionsAsync(string recipientId);
    }
}
