using MediatR;
using Microsoft.AspNetCore.Identity;
using Swapy.BLL.Domain.Users.Commands;
using Swapy.Common.Entities;
using Swapy.Common.Exceptions;

namespace Swapy.BLL.Domain.Users.CommandHandlers
{
    public class UpdateMessagesStatusCommandHandler : IRequestHandler<UpdateMessagesStatusCommand, Unit>
    {
        private UserManager<User> _userManager;

        public UpdateMessagesStatusCommandHandler(UserManager<User> userManager) => _userManager = userManager;

        public async Task<Unit> Handle(UpdateMessagesStatusCommand request, CancellationToken cancellationToken)
        {
            var user = await _userManager.FindByIdAsync(request.UserId);

            if (user == null) throw new NotFoundException($"User with {request.UserId} not found");

            user.HasUnreadMessages = request.Value;
            
            await _userManager.UpdateAsync(user);

            return Unit.Value;
        }
    }
}