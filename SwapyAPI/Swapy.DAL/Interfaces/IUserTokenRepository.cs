using Swapy.Common.Entities;

namespace Swapy.DAL.Interfaces
{
    public interface IUserTokenRepository : IRepository<UserToken>
    {
        Task<UserToken> GetByAccessTokenAsync(string accessToken);
    }
}
