using Swapy.Common.DTO.Products.Requests.Queries;
using Swapy.Common.Entities;

namespace Swapy.Common.DTO.Autos.Requests.Queries
{
    public class GetAllAutoAttributesQueryDTO : GetAllBasicProductsQueryDTO<AutoAttribute>
    {
        public int? MiliageMin { get; set; }
        public int? MiliageMax { get; set; }
        public int? EngineCapacityMin { get; set; }
        public int? EngineCapacityMax { get; set; }
        public DateTime? ReleaseYearOlder { get; set; }
        public DateTime? ReleaseYearNewer { get; set; }
        public bool? IsNew { get; set; }
        public List<string>? FuelTypesId { get; set; }
        public List<string>? AutoColorsId { get; set; }
        public List<string>? TransmissionTypesId { get; set; }
        public List<string>? AutoBrandsId { get; set; }
        public List<string>? AutoTypesId { get; set; }
    }
}
