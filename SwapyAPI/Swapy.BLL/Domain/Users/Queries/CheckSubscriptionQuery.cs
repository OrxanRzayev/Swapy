using MediatR;

namespace Swapy.BLL.Domain.Users.Queries
{
    public class CheckSubscriptionQuery : IRequest<bool>
    {
        public string UserId { get; set; }
        public string RecipientId { get; set; }
    }
}
