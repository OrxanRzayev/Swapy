using MediatR;
using Swapy.Common.Entities;

namespace Swapy.BLL.Domain.Users.Queries
{
    public class GetUserSubscriptionsQuery : IRequest<IEnumerable<Subscription>>
    {
        public string UserId { get; set; }
    }
}
