using MediatR;
using Microsoft.AspNetCore.Identity;
using Swapy.BLL.Domain.Auth.Commands;
using Swapy.BLL.Interfaces;
using Swapy.Common.Entities;
using Swapy.Common.Exceptions;
using System.Web;

namespace Swapy.BLL.Domain.Auth.CommandHandlers
{
    public class ResetPasswordCommandHandler : IRequestHandler<ResetPasswordCommand, Unit>
    {
        private readonly IEmailService _emailService;
        private readonly UserManager<User> _userManager;

        public ResetPasswordCommandHandler(UserManager<User> userManager, IEmailService emailService)
        {
            _userManager = userManager;
            _emailService = emailService;
        }

        public async Task<Unit> Handle(ResetPasswordCommand request, CancellationToken cancellationToken)
        {
            var user = await _userManager.FindByIdAsync(request.UserId);

            if (user == null) throw new NotFoundException($"User with {request.UserId} id not found");

            if (!user.EmailConfirmed) throw new UnconfirmedEmailException("Email is not confirmed");

            request.Token = HttpUtility.HtmlDecode(request.Token);
            request.Token = request.Token.Replace(" ", "+");

            var isValidToken = await _userManager.VerifyUserTokenAsync(user, _userManager.Options.Tokens.PasswordResetTokenProvider, "ResetPassword", request.Token);

            if (!isValidToken) throw new TokenExpiredException("Provided token already expired");

            var result = await _userManager.ResetPasswordAsync(user, request.Token, request.Password);

            if (!result.Succeeded) throw new InvalidOperationException("An error occurred while reset the password");

            await _emailService.SendResetPasswordAsync(user.Email);

            return Unit.Value;
        }
    }
}
