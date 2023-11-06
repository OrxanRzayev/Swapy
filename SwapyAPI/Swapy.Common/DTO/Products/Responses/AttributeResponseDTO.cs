using Swapy.Common.Enums;
using Swapy.Common.Models;

namespace Swapy.Common.DTO.Products.Responses
{
    public abstract class AttributeResponseDTO
    {
        public string Id { get; set; }
        public string City { get; set; }
        public string CityId { get; set; }
        public string Currency { get; set; }
        public string CurrencyId { get; set; }
        public string CurrencySymbol { get; set; }
        public string UserId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string PhoneNumber { get; set; }
        public string ShopId { get; set; }
        public bool IsDisable { get; set; }
        public bool IsFavorite { get; set; }
        public string Shop { get; set; }
        public UserType UserType { get; set; }
        public string ProductId { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public int Views { get; set; }
        public decimal Price { get; set; }
        public DateTime DateTime { get; set; }
        public List<SpecificationResponseDTO<string>> Categories { get; set; }
        public List<string> Images { get; set; }
    }
}
