using Swapy.Common.DTO.Products.Requests.Queries;
using Swapy.Common.Entities;

namespace Swapy.Common.DTO.Electronics.Requests.Queries
{
    public class GetAllElectronicAttributesQueryDTO : GetAllBasicProductsQueryDTO<ElectronicAttribute>
    {
        public bool? IsNew { get; set; }
        public List<string>? MemoriesId { get; set; }
        public List<string>? ColorsId { get; set; }
        public List<string>? ModelsId { get; set; }
        public List<string>? BrandsId { get; set; }
        public List<string>? TypesId { get; set; }
    }
}
