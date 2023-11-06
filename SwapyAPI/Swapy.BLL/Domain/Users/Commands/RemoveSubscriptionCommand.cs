using MediatR;

namespace Swapy.BLL.Domain.Users.Commands
{
    public class RemoveSubscriptionCommand : IRequest<Unit>
    {
        public string SubscriberId { get; set; }
        public string RecipientId { get; set; }
    }
}
