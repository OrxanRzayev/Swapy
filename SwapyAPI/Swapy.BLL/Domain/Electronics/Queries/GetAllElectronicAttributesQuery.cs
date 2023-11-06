using Swapy.BLL.Domain.Products.Queries;
using Swapy.Common.DTO.Products.Responses;

namespace Swapy.BLL.Domain.Electronics.Queries
{
    public class GetAllElectronicAttributesQuery : GetAllProductQuery<ProductResponseDTO>
    {
        public bool? IsNew { get; set; }
        public List<string>? MemoriesId { get; set; }
        public List<string>? ColorsId { get; set; }
        public List<string>? ModelsId { get; set; }
        public List<string>? BrandsId { get; set; }
        public List<string>? TypesId { get; set; }
    }
}
