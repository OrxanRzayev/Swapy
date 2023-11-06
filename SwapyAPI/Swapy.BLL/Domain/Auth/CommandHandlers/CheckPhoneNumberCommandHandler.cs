using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Swapy.BLL.Domain.Auth.Commands;
using Swapy.Common.Entities;

namespace Swapy.BLL.Domain.Auth.CommandHandlers
{
    public class CheckPhoneNumberCommandHandler : IRequestHandler<PhoneNumberCommand, bool>
    {
        private readonly UserManager<User> _userManager;

        public CheckPhoneNumberCommandHandler(UserManager<User> userManager) => _userManager = userManager;

        public async Task<bool> Handle(PhoneNumberCommand request, CancellationToken cancellationToken)
        {
            var result = await _userManager.Users.FirstOrDefaultAsync(u => u.PhoneNumber.Equals(request.PhoneNumber));
            if (result == null) return false;
            return true;
        }
    }
}
