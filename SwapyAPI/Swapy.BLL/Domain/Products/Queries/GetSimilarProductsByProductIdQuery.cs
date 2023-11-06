using MediatR;
using Swapy.Common.DTO.Products.Responses;

namespace Swapy.BLL.Domain.Products.Queries
{
    public class GetSimilarProductsByProductIdQuery : IRequest<ProductsResponseDTO<ProductResponseDTO>>
    {
        public string ProductId { get; set; }
        public string UserId { get; set; }
        public int Page { get; set; }
        public int PageSize { get; set; }
        public string? Title { get; set; }
        public string? CurrencyId { get; set; }
        public decimal? PriceMin { get; set; }
        public decimal? PriceMax { get; set; }
        public string? CategoryId { get; set; }
        public string? SubcategoryId { get; set; }
        public string? CityId { get; set; }
    }
}
