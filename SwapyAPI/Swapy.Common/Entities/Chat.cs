namespace Swapy.Common.Entities
{
    public class Chat
    {
        public string Id { get; set; }
        public string BuyerId { get; set; }
        public User Buyer { get; set; }
        public string ProductId { get; set; }
        public Product Product { get; set; }
        public bool IsReaded { get; set; }
        public ICollection<Message> Messages { get; set; } = new List<Message>();

        public Chat() => Id = Guid.NewGuid().ToString();

        public Chat(string productId, string buyerId) : this()
        {
            ProductId = productId;
            BuyerId = buyerId;
            IsReaded = true;
        }
    }
}
