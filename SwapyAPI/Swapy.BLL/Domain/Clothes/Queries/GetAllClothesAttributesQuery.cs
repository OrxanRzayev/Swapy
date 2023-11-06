using Swapy.BLL.Domain.Products.Queries;
using Swapy.Common.DTO.Products.Responses;

namespace Swapy.BLL.Domain.Clothes.Queries
{
    public class GetAllClothesAttributesQuery : GetAllProductQuery<ProductResponseDTO>
    {
        public bool? IsNew { get; set; }
        public List<string>? ClothesSeasonsId { get; set; }
        public List<string>? ClothesSizesId { get; set; }
        public List<string>? ClothesBrandsId { get; set; }
        public List<string>? ClothesViewsId { get; set; }
        public List<string>? ClothesTypesId { get; set; }
        public List<string>? ClothesGendersId { get; set; }
        public bool? IsChild { get; set; }
    }
}
