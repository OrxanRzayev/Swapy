using Swapy.Common.Entities;

namespace Swapy.BLL.Interfaces
{
    public interface IUserTokenService
    {
        Task<string> GenerateRefreshToken();
        Task<string> GenerateJwtToken(string userId, string email, string firstName, string lastname);
    }
}
