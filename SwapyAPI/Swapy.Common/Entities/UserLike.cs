namespace Swapy.Common.Entities
{
    public class UserLike
    {
        public string Id { get; set; }
        public string RecipientId { get; set; }
        public User Recipient { get; set; }
        public string LikeId { get; set; }
        public Like Like { get; set; }

        public UserLike() => Id = Guid.NewGuid().ToString();

        public UserLike(string recipientId, string likeId) : this()
        {
            RecipientId = recipientId;
            LikeId = likeId;
        }
    }
}
