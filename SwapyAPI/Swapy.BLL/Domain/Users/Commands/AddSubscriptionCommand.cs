using MediatR;
using Swapy.Common.Entities;
using Swapy.Common.Enums;

namespace Swapy.BLL.Domain.Users.Commands
{
    public class AddSubscriptionCommand : IRequest<Subscription>
    {
        public string UserId { get; set; }
        public UserType Type { get; set; }
        public string RecipientId { get; set; }
    }
}
