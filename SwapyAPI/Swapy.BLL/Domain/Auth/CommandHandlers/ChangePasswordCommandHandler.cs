using MediatR;
using Microsoft.AspNetCore.Identity;
using Swapy.BLL.Domain.Auth.Commands;
using Swapy.BLL.Interfaces;
using Swapy.Common.Entities;

namespace Swapy.BLL.Domain.Auth.CommandHandlers
{
    public class ChangePasswordCommandHandler : IRequestHandler<ChangePasswordCommand, Unit>
    {
        private readonly IEmailService _emailService;
        private readonly UserManager<User> _userManager;

        public ChangePasswordCommandHandler(UserManager<User> userManager, IEmailService emailService)
        {
            _userManager = userManager;
            _emailService = emailService;
        }

        public async Task<Unit> Handle(ChangePasswordCommand request, CancellationToken cancellationToken)
        {
            var user = await _userManager.FindByIdAsync(request.UserId);

            if (!await _userManager.CheckPasswordAsync(user, request.OldPassword)) throw new InvalidOperationException("The previous password is not correct");

            if (await _userManager.CheckPasswordAsync(user, request.NewPassword)) throw new InvalidOperationException("The new password cannot be the same as the previous one");

            var changePasswordResult = await _userManager.ChangePasswordAsync(user, request.OldPassword, request.NewPassword);

            if (!changePasswordResult.Succeeded) throw new InvalidOperationException("An error occurred while changing the password");

            await _emailService.SendPasswordChangedSuccessfullyAsync(user.Email);

            return Unit.Value;
        }
    }
}
