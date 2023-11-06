namespace Swapy.Common.Entities
{
    public class ProductImage
    {
        public string Id { get; set; }
        public string Image { get; set; }
        public string ProductId { get; set; }
        public Product Product { get; set; }

        public ProductImage() => Id = Guid.NewGuid().ToString();

        public ProductImage(string image, string productId) : this()
        {
            Image = image;
            ProductId = productId;
        }
    }
}
