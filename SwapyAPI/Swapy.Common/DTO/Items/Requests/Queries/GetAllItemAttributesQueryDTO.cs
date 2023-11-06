using Swapy.Common.DTO.Products.Requests.Queries;
using Swapy.Common.Entities;

namespace Swapy.Common.DTO.Items.Requests.Queries
{
    public class GetAllItemAttributesQueryDTO : GetAllBasicProductsQueryDTO<ItemAttribute>
    {
        public bool? IsNew { get; set; }
        public List<string>? ItemTypesId { get; set; }
    }
}
