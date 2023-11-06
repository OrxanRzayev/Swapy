using MediatR;

namespace Swapy.BLL.Domain.Users.Commands
{
    public class ToggleSubscriptionStatusCommand : IRequest<Unit>
    {
        public string UserId { get; set; }
    }
}
