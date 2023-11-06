using MediatR;
using Microsoft.AspNetCore.Identity;
using Swapy.BLL.Domain.Auth.Commands;
using Swapy.BLL.Interfaces;
using Swapy.Common.DTO.Auth.Responses;
using Swapy.Common.Entities;
using Swapy.Common.Exceptions;
using Swapy.DAL.Interfaces;

namespace Swapy.BLL.Domain.Auth.CommandHandlers
{
    public class UpdateUserTokenCommandHandler : IRequestHandler<UpdateUserTokenCommand, AuthResponseDTO>
    {
        private readonly UserManager<User> _userManager;
        private readonly IUserTokenService _userTokenService;
        private readonly IUserTokenRepository _userTokenRepository;

        public UpdateUserTokenCommandHandler(UserManager<User> userManager, IUserTokenRepository refreshTokenRepository, IUserTokenService userTokenService)
        {
            _userManager = userManager;
            _userTokenService = userTokenService;
            _userTokenRepository = refreshTokenRepository;
        }

        public async Task<AuthResponseDTO> Handle(UpdateUserTokenCommand request, CancellationToken cancellationToken)
        {
            var userToken = await _userTokenRepository.GetByAccessTokenAsync(request.OldAccessToken);

            if (userToken.ExpiresAt < DateTime.UtcNow) throw new TokenExpiredException("The provided Refresh token has expired");

            var user = await _userManager.FindByIdAsync(request.UserId);
            
            userToken.AccessToken = await _userTokenService.GenerateJwtToken(user.Id, user.Email, user.FirstName, user.LastName);
            
            await _userTokenRepository.UpdateAsync(userToken);

            return new AuthResponseDTO { UserId = user.Id, Type = user.Type, AccessToken = userToken.AccessToken, RefreshToken = userToken.RefreshToken, HasUnreadMessages = user.HasUnreadMessages };
        }
    }
}
