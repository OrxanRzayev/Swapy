using Swapy.Common.DTO.Products.Requests.Queries;
using Swapy.Common.Entities;

namespace Swapy.Common.DTO.Clothes.Requests.Queries
{
    public class GetAllClothesAttributesQueryDTO : GetAllBasicProductsQueryDTO<ClothesAttribute>
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
