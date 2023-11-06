using Swapy.BLL.Domain.Products.Queries;
using Swapy.Common.DTO.Products.Responses;

namespace Swapy.BLL.Domain.TVs.Queries
{
    public class GetAllTVAttributesQuery : GetAllProductQuery<ProductResponseDTO>
    {
        public bool? IsNew { get; set; }
        public bool? IsSmart { get; set; }
        public List<string>? TVTypesId { get; set; }
        public List<string>? TVBrandsId { get; set; }
        public List<string>? ScreenResolutionsId { get; set; }
        public List<string>? ScreenDiagonalsId { get; set; }
    }
}
