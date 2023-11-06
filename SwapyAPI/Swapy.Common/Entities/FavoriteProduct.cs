namespace Swapy.Common.Entities
{
    public class FavoriteProduct
    {
        public string Id { get; set; }
        public string UserId { get; set; }
        public User User { get; set; }
        public string ProductId { get; set; }
        public Product Product { get; set; }

        public FavoriteProduct() => Id = Guid.NewGuid().ToString();

        public FavoriteProduct(string userId, string productId) : this()
        {
            UserId = userId;
            ProductId = productId;
        }
    }
}
