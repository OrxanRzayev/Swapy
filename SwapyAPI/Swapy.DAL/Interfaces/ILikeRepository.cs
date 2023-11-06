using Swapy.Common.Entities;

namespace Swapy.DAL.Interfaces
{
    public interface ILikeRepository : IRepository<Like>
    {
        Task<Like> GetByUserIdAsync (string userId);
    }
}
