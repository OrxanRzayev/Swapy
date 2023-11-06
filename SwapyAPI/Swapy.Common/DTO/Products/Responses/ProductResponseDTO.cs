using Swapy.Common.Enums;

namespace Swapy.Common.DTO.Products.Responses
{
    public class ProductResponseDTO
    {
        public string Id { get; set; }
        public string Title { get; set; }
        public decimal Price { get; set; }
        public string City { get; set; }
        public string Currency { get; set; }
        public string CurrencySymbol { get; set; }
        public DateTime DateTime { get; set; }
        public List<string> Images { get; set; }
        public UserType UserType { get; set; }
        public string UserId { get; set; }
        public CategoryType Type { get; set; }
        public bool IsFavorite { get; set; }
        public bool IsDisable { get; set; }
    }
}
