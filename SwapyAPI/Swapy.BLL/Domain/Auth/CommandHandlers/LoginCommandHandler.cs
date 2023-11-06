using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Swapy.BLL.Domain.Auth.Commands;
using Swapy.BLL.Interfaces;
using Swapy.Common.DTO.Auth.Responses;
using Swapy.Common.Entities;
using Swapy.Common.Exceptions;
using Swapy.DAL.Interfaces;
using System.Security.Authentication;

namespace Swapy.BLL.Domain.Auth.CommandHandlers
{
    public class LoginCommandHandler : IRequestHandler<LoginCommand, AuthResponseDTO>
    {
        private readonly UserManager<User> _userManager;
        private readonly SignInManager<User> _signInManager;
        private readonly IUserTokenService _userTokenService;
        private readonly IUserTokenRepository _userTokenRepository;

        public LoginCommandHandler(UserManager<User> userManager, SignInManager<User> signInManager, IUserTokenRepository userTokenRepository, IUserTokenService userTokenService)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _userTokenService = userTokenService;
            _userTokenRepository = userTokenRepository;
        }

        public async Task<AuthResponseDTO> Handle(LoginCommand request, CancellationToken cancellationToken)
        {
            var user = await _userManager.FindByEmailAsync(request.EmailOrPhone);

            if (user == null)
            {
                user = await _userManager.Users.FirstOrDefaultAsync(u => u.PhoneNumber.Equals(request.EmailOrPhone));
                if (user == null) throw new NotFoundException("Invalid email, phone number or password");
            }

            if (!user.EmailConfirmed) throw new UnconfirmedEmailException("Confirm email before login");

            var result = await _signInManager.CheckPasswordSignInAsync(user, request.Password, false);
            if (!result.Succeeded) throw new AuthenticationException("Invalid email, phone number or password");

            if (!string.IsNullOrEmpty(user.UserTokenId)) await _userTokenRepository.DeleteByIdAsync(user.UserTokenId);

            var accessToken = await _userTokenService.GenerateJwtToken(user.Id, user.Email, user.FirstName, user.LastName);
            var refreshToken = await _userTokenService.GenerateRefreshToken();

            user.UserTokenId = refreshToken;
            await _userTokenRepository.CreateAsync(new UserToken(accessToken, refreshToken, DateTime.UtcNow.AddDays(30), user.Id));
            await Console.Out.WriteLineAsync(user.HasUnreadMessages.ToString());
            return new AuthResponseDTO { Type = user.Type, UserId = user.Id, AccessToken = accessToken, RefreshToken = refreshToken, HasUnreadMessages = user.HasUnreadMessages };
        }
    }
}
