using MediatR;
using Microsoft.AspNetCore.Identity;
using Swapy.BLL.Domain.Users.Commands;
using Swapy.Common.Entities;
using Swapy.Common.Exceptions;

namespace Swapy.BLL.Domain.Users.CommandHandlers
{
    public class UpdateUserCommandHandler : IRequestHandler<UpdateUserCommand, Unit>
    {
        private readonly UserManager<User> _userManager;
        
        public UpdateUserCommandHandler(UserManager<User> userManager) => _userManager = userManager;

        public async Task<Unit> Handle(UpdateUserCommand request, CancellationToken cancellationToken)
        {
            var user = await _userManager.FindByIdAsync(request.UserId);

            if (user == null) throw new NoAccessException("No access to update this user");

            if(!string.IsNullOrEmpty(request.LastName)) user.LastName = request.LastName;
            if(!string.IsNullOrEmpty(request.FirstName)) user.FirstName = request.FirstName;
            if (!string.IsNullOrEmpty(request.PhoneNumber) && !user.PhoneNumber.Equals(request.PhoneNumber)) user.PhoneNumber = request.PhoneNumber;
            user.IsSubscribed = request.IsSubscribed;

            await _userManager.UpdateAsync(user);

            return Unit.Value;
        }
    }
}