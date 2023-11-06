using Swapy.Common.DTO.Products.Requests.Commands;

namespace Swapy.Common.DTO.Items.Requests.Commands
{
    public class UpdateItemAttributeCommandDTO : UpdateProductCommandDTO
    {
        public bool? IsNew { get; set; }
        public string? ItemTypeId { get; set; }
    }
}
