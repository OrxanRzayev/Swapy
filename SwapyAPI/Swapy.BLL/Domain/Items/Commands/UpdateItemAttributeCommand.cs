using Swapy.BLL.Domain.Products.Commands;

namespace Swapy.BLL.Domain.Items.Commands
{
    public class UpdateItemAttributeCommand : UpdateProductCommand
    {
        public bool? IsNew { get; set; }
        public string ItemTypeId { get; set; }
    }
}
