using MediatR;
using Microsoft.AspNetCore.Identity;
using Swapy.BLL.Domain.Users.Commands;
using Swapy.Common.Entities;
using Swapy.Common.Enums;
using Swapy.Common.Exceptions;
using Swapy.DAL.Interfaces;
using Swapy.DAL.Repositories;

namespace Swapy.BLL.Domain.Users.CommandHandlers
{
    public class AddSubscriptionCommandHandler : IRequestHandler<AddSubscriptionCommand, Subscription>
    {
        private readonly UserManager<User> _userManager;
        private readonly ISubscriptionRepository _subscriptionRepository;
        private readonly IUserSubscriptionRepository _userSubscriptionRepository;

        public AddSubscriptionCommandHandler(ISubscriptionRepository subscriptionRepository, IUserSubscriptionRepository userSubscriptionRepository, UserManager<User> userManager)
        {
            _userManager = userManager;
            _subscriptionRepository = subscriptionRepository;
            _userSubscriptionRepository = userSubscriptionRepository;
        }

        public async Task<Subscription> Handle(AddSubscriptionCommand request, CancellationToken cancellationToken)
        {
            if (await _userSubscriptionRepository.CheckUserSubscriptionAsync(request.UserId, request.RecipientId)) throw new DuplicateValueException("The provided SubscriberId already subscribed Recepient");

            if (request.UserId.Equals(request.RecipientId)) throw new DuplicateValueException("The provided SubscriberId and RecipientId are the same");

            if (request.Type != UserType.Seller) throw new InvalidOperationException("The provided item Id can't subscribe other users");

            var subscribe = new Subscription() { SubscriberId = request.UserId };
            var userSubscription = new UserSubscription(request.RecipientId, subscribe.Id);
            await _userSubscriptionRepository.CreateAsync(userSubscription);

            subscribe.UserSubscriptionId = userSubscription.Id;
            await _subscriptionRepository.CreateAsync(subscribe);

            var user = await _userManager.FindByIdAsync(request.RecipientId);
            user.SubscriptionsCount++;
            await _userManager.UpdateAsync(user);

            return subscribe;
        }
    }
}
