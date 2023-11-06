using Microsoft.AspNetCore.Http;

namespace Swapy.Common.DTO.Products.Requests.Commands
{
    public class UpdateProductCommandDTO
    {
        public string? ProductId { get; set; }
        public string? Title { get; set; }
        public string? Description { get; set; }
        public decimal? Price { get; set; }
        public string? CurrencyId { get; set; }
        public string? CategoryId { get; set; }
        public string? SubcategoryId { get; set; }
        public string? CityId { get; set; }
        public List<string>? OldPaths { get; set; }
    }
}
