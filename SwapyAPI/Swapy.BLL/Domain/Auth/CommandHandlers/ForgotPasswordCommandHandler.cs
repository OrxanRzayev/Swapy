using MediatR;
using Microsoft.AspNetCore.Identity;
using Swapy.BLL.Domain.Auth.Commands;
using Swapy.BLL.Interfaces;
using Swapy.Common.Entities;
using Swapy.Common.Exceptions;
using System.Web;

namespace Swapy.BLL.Domain.Auth.CommandHandlers
{
    public class ForgotPasswordCommandHandler : IRequestHandler<ForgotPasswordCommand, Unit>
    {
        private readonly IEmailService _emailService;
        private readonly UserManager<User> _userManager;
        private readonly IKeyVaultService _keyVaultService;

        public ForgotPasswordCommandHandler(IEmailService emailService, UserManager<User> userManager, IKeyVaultService keyVaultService)
        {
            _emailService = emailService;
            _userManager = userManager;
            _keyVaultService = keyVaultService;
        }

        public async Task<Unit> Handle(ForgotPasswordCommand request, CancellationToken cancellationToken)
        {
            var user = await _userManager.FindByEmailAsync(request.Email);

            if (user == null) throw new NotFoundException($"User with {request.Email} email not found");

            if(!user.EmailConfirmed) throw new UnconfirmedEmailException("Email is not confirmed");

            var resetToken = await _userManager.GeneratePasswordResetTokenAsync(user);
            resetToken = HttpUtility.HtmlEncode(resetToken);
            var webUrl = await _keyVaultService.GetSecretValue("Web-Url");
            var callbackUrl = new UriBuilder(webUrl);
            callbackUrl.Path = "/auth/reset-password";
            callbackUrl.Query = $"userid={user.Id}&token={resetToken}";
            await _emailService.SendForgotPasswordAsync(user.Email, callbackUrl.Uri.ToString());

            return Unit.Value;
        }
    }
}
