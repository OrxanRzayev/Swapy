using Swapy.Common.DTO.Products.Requests.Queries;
using Swapy.Common.Entities;

namespace Swapy.Common.DTO.RealEstates.Requests.Queries
{
    public class GetAllRealEstateAttributesQueryDTO : GetAllBasicProductsQueryDTO<RealEstateAttribute>
    {
        public int? AreaMin { get; set; }
        public int? AreaMax { get; set; }
        public int? RoomsMin { get; set; }
        public int? RoomsMax { get; set; }
        public bool? IsRent { get; set; }
        public List<string>? RealEstateTypesId { get; set; }
    }
}
