namespace Swapy.Common.Entities
{
    public class UserSubscription
    {
        public string Id { get; set; }
        public string RecipientId { get; set; }
        public User Recipient { get; set; }
        public string SubscriptionId { get; set; }
        public Subscription Subscription { get; set; }

        public UserSubscription() => Id = Guid.NewGuid().ToString();

        public UserSubscription(string recipientId, string subscriptionId) : this()
        {
            RecipientId = recipientId;
            SubscriptionId = subscriptionId;
        }
    }
}
