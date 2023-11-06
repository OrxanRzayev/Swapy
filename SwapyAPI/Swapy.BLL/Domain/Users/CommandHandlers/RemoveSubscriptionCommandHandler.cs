using MediatR;
using Microsoft.AspNetCore.Identity;
using Swapy.BLL.Domain.Users.Commands;
using Swapy.Common.Entities;
using Swapy.Common.Exceptions;
using Swapy.DAL.Interfaces;

namespace Swapy.BLL.Domain.Users.CommandHandlers
{
    public class RemoveSubscriptionCommandHandler : IRequestHandler<RemoveSubscriptionCommand, Unit>
    {
        private readonly UserManager<User> _userManager;
        private readonly IUserSubscriptionRepository _userSubscriptionRepository;

        public RemoveSubscriptionCommandHandler(IUserSubscriptionRepository userSubscriptionRepository, UserManager<User> userManager)
        {
            _userManager = userManager;
            _userSubscriptionRepository = userSubscriptionRepository;
        }

        public async Task<Unit> Handle(RemoveSubscriptionCommand request, CancellationToken cancellationToken)
        {
            var userSubscription = await _userSubscriptionRepository.GetUserSubscriptionByRecipientAsync(request.SubscriberId, request.RecipientId);

            if (userSubscription.Subscription.SubscriberId != request.SubscriberId) throw new NoAccessException("No access to delete this subscription");


            var user = await _userManager.FindByIdAsync(request.RecipientId);
            user.SubscriptionsCount--;
            await _userSubscriptionRepository.DeleteAsync(userSubscription);

            return Unit.Value;
        }
    }
}
