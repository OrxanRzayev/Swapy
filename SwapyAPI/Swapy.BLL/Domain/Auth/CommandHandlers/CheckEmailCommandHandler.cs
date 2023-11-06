using MediatR;
using Microsoft.AspNetCore.Identity;
using Swapy.BLL.Domain.Auth.Commands;
using Swapy.Common.Entities;

namespace Swapy.BLL.Domain.Auth.CommandHandlers
{
    public class CheckEmailCommandHandler : IRequestHandler<EmailCommand, bool>
    {
        private readonly UserManager<User> _userManager;
        
        public CheckEmailCommandHandler(UserManager<User> userManager) => _userManager = userManager;

        public async Task<bool> Handle(EmailCommand request, CancellationToken cancellationToken)
        {
            var result = await _userManager.FindByEmailAsync(request.Email);
            if(result == null) return false;
            return true;
        }
    }
}
