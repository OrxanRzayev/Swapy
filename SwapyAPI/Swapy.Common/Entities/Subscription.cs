namespace Swapy.Common.Entities
{
    public class Subscription
    {
        public string Id { get; set; }
        public string UserSubscriptionId { get; set; }
        public UserSubscription UserSubscription { get; set; }
        public string SubscriberId { get; set; }
        public User Subscriber { get; set; }

        public Subscription() => Id = Guid.NewGuid().ToString();

        public Subscription(string userSubscriptionId, string subscriberId) : this()
        {
            SubscriberId = subscriberId;
            UserSubscriptionId = userSubscriptionId;
        }
    }
}
