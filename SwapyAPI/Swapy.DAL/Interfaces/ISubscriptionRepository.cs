using Swapy.Common.Entities;

namespace Swapy.DAL.Interfaces
{
    public interface ISubscriptionRepository : IRepository<Subscription>
    {
        Task<IEnumerable<Subscription>> GetAllByUserIdAsync(string userId);
    }
}
