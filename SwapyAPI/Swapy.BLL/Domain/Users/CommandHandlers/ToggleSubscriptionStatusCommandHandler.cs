using MediatR;
using Microsoft.AspNetCore.Identity;
using Swapy.BLL.Domain.Users.Commands;
using Swapy.Common.Entities;
using Swapy.Common.Enums;

namespace Swapy.BLL.Domain.Users.CommandHandlers
{
    public class ToggleSubscriptionStatusCommandHandler : IRequestHandler<ToggleSubscriptionStatusCommand, Unit>
    {
        private readonly UserManager<User> _userManager;

        public ToggleSubscriptionStatusCommandHandler(UserManager<User> userManager) => _userManager = userManager;

        public async Task<Unit> Handle(ToggleSubscriptionStatusCommand request, CancellationToken cancellationToken)
        {
            var user = await _userManager.FindByIdAsync(request.UserId);
            
            if (user.Type.Equals(UserType.Seller))
            {
                user.IsSubscribed = !user.IsSubscribed;

                await _userManager.UpdateAsync(user);
            }

            return Unit.Value;
        }
    }
}
