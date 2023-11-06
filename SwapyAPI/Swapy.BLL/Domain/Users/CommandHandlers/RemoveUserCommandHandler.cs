using MediatR;
using Microsoft.AspNetCore.Identity;
using Swapy.BLL.Domain.Users.Commands;
using Swapy.BLL.Interfaces;
using Swapy.Common.Entities;
using Swapy.Common.Exceptions;

namespace Swapy.BLL.Domain.Users.CommandHandlers
{
    public class RemoveUserCommandHandler : IRequestHandler<RemoveUserCommand, Unit>
    {
        private readonly IEmailService _emailService;
        private readonly UserManager<User> _userManager;

        public RemoveUserCommandHandler(UserManager<User> userManager, IEmailService emailService)
        {
            _userManager = userManager;
            _emailService = emailService;
        }

        public async Task<Unit> Handle(RemoveUserCommand request, CancellationToken cancellationToken)
        {
            var user = await _userManager.FindByIdAsync(request.UserId);

            var isValidToken = await _userManager.VerifyUserTokenAsync(user, "UserTokenProvider", "DeleteUser", request.Token);

            if (!isValidToken) throw new TokenExpiredException("Provided token already expired");

            var result = await _userManager.DeleteAsync(user);

            if (result.Succeeded) await _emailService.SendRemovedAsync(user.Email);

            return Unit.Value;
        }
    }
}
