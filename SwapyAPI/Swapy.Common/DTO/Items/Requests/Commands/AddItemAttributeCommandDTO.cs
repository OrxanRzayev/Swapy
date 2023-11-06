using Swapy.Common.DTO.Products.Requests.Commands;
using Swapy.Common.Entities;

namespace Swapy.Common.DTO.Items.Requests.Commands
{
    public class AddItemAttributeCommandDTO : AddProductCommandDTO
    {
        public bool IsNew { get; set; }
        public string ItemTypeId { get; set; }
    }
}
