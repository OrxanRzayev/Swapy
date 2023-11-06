using MediatR;
using Microsoft.AspNetCore.Identity;
using Swapy.BLL.Domain.Auth.Commands;
using Swapy.Common.Entities;
using Swapy.Common.Exceptions;
using System.Web;

namespace Swapy.BLL.Domain.Auth.CommandHandlers
{
    public class ConfirmEmailCommandHandler : IRequestHandler<ConfirmEmailCommand, Unit>
    {
        private readonly UserManager<User> _userManager;

        public ConfirmEmailCommandHandler(UserManager<User> userManager) => _userManager = userManager;

        public async Task<Unit> Handle(ConfirmEmailCommand request, CancellationToken cancellationToken)
        {
            var user = await _userManager.FindByIdAsync(request.UserId);
            
            if (user == null) throw new NotFoundException($"UserId {request.UserId} not found");

            if (user.EmailConfirmed) throw new InvalidOperationException($"Invalid operation");

            request.Token = HttpUtility.HtmlDecode(request.Token);
            request.Token = request.Token.Replace(" ", "+");

            var isValidToken = await _userManager.VerifyUserTokenAsync(user, _userManager.Options.Tokens.EmailConfirmationTokenProvider, "EmailConfirmation", request.Token);

            if (!isValidToken) throw new TokenExpiredException("Provided token already expired");

            var result = await _userManager.ConfirmEmailAsync(user, request.Token);
            
            if (result.Succeeded) return Unit.Value;
            else throw new Exception("During email confirmation");
        }
    }
}
