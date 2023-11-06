using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Swapy.BLL.Domain.Users.Commands;
using Swapy.BLL.Interfaces;
using Swapy.Common.Entities;

namespace Swapy.BLL.Domain.Users.CommandHandlers
{
    public class SendMessageToRemoveCommandHandler : IRequestHandler<SendMessageToRemoveCommand, Unit>
    {
        private readonly IEmailService _emailService;
        private readonly IConfiguration _configuration;
        private readonly UserManager<User> _userManager;
        
        public SendMessageToRemoveCommandHandler(UserManager<User> userManager, IEmailService emailService, IConfiguration configuration)
        {
            _userManager = userManager;
            _emailService = emailService;
            _configuration = configuration;
        }
        
        public async Task<Unit> Handle(SendMessageToRemoveCommand request, CancellationToken cancellationToken)
        {
            var user = await _userManager.FindByIdAsync(request.UserId);

            var deleteToken = await _userManager.GenerateUserTokenAsync(user, "UserTokenProvider", "DeleteUser");

            var callbackUrl = new UriBuilder(_configuration["WebUrl"]);
            callbackUrl.Path = "/users/delete";
            callbackUrl.Query = $"token={deleteToken}";
            await _emailService.SendTryRemoveUserAsync(user.Email, callbackUrl.Uri.ToString());

            return Unit.Value;
        }
    }
}
