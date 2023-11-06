using Microsoft.IdentityModel.Tokens;
using Swapy.BLL.Interfaces;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Swapy.BLL.Services
{
    public class UserTokenService : IUserTokenService
    {
        private readonly IKeyVaultService _keyVaultService;

        public UserTokenService(IKeyVaultService keyVaultService) => _keyVaultService = keyVaultService;

        public async Task<string> GenerateRefreshToken() => Guid.NewGuid().ToString();

        public async Task<string> GenerateJwtToken(string userId, string email, string firstName, string lastname)
        {
            var guid = await _keyVaultService.GetSecretValue("JWT-Key");
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(guid));
            var credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new Claim[]
                {
                    new Claim(JwtRegisteredClaimNames.Sub, userId),
                    new Claim(JwtRegisteredClaimNames.Email, email),
                    new Claim(JwtRegisteredClaimNames.Name, firstName ?? string.Empty),
                    new Claim(JwtRegisteredClaimNames.FamilyName, lastname ?? string.Empty),
                    new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                }),
                Expires = DateTime.UtcNow.AddHours(24),
                SigningCredentials = credentials
            };
            var token = tokenHandler.CreateToken(tokenDescriptor);
            var accessToken = tokenHandler.WriteToken(token);
            return accessToken;
        }
    }
}