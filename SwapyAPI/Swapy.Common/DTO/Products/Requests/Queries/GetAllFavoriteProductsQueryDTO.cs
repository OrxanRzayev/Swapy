using Swapy.Common.Entities;

namespace Swapy.Common.DTO.Products.Requests.Queries
{
    public class GetAllFavoriteProductsQueryDTO : GetAllBasicProductsQueryDTO<FavoriteProduct>
    {
        public string? ProductId { get; set; }
    }
}
