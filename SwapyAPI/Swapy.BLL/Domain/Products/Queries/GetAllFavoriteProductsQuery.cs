using Swapy.Common.DTO.Products.Responses;

namespace Swapy.BLL.Domain.Products.Queries
{
    public class GetAllFavoriteProductsQuery : GetAllProductQuery<ProductResponseDTO>
    {
        public string ProductId { get; set; }
    }
}
