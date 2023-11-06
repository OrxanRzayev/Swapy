using MediatR;
using Swapy.BLL.Domain.Users.Queries;
using Swapy.Common.Entities;
using Swapy.DAL.Interfaces;

namespace Swapy.BLL.Domain.Users.QueryHandlers
{
    public class GetUserSubscriptionsQueryHandler : IRequestHandler<GetUserSubscriptionsQuery, IEnumerable<Subscription>>
    {
        private readonly ISubscriptionRepository _subscriptionRepository;

        public GetUserSubscriptionsQueryHandler(ISubscriptionRepository subscriptionRepository) => _subscriptionRepository = subscriptionRepository;

        public async Task<IEnumerable<Subscription>> Handle(GetUserSubscriptionsQuery request, CancellationToken cancellationToken)
        {
            return await _subscriptionRepository.GetAllByUserIdAsync(request.UserId);
        }
    }
}
