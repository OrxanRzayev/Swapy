using Swapy.Common.DTO.Products.Requests.Queries;
using Swapy.Common.Entities;

namespace Swapy.Common.DTO.TVs.Requests.Queries
{
    public class GetAllTVAttributesQueryDTO : GetAllBasicProductsQueryDTO<TVAttribute>
    {
        public bool? IsNew { get; set; }
        public bool? IsSmart { get; set; }
        public List<string>? TvTypesId { get; set; }
        public List<string>? TvBrandsId { get; set; }
        public List<string>? ScreenResolutionsId { get; set; }
        public List<string>? ScreenDiagonalsId { get; set; }
    }
}
