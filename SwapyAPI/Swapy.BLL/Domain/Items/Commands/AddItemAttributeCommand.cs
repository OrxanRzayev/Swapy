using Swapy.BLL.Domain.Products.Commands;
using Swapy.Common.Entities;

namespace Swapy.BLL.Domain.Items.Commands
{
    public class AddItemAttributeCommand : AddProductCommand<ItemAttribute>
    {
        public bool IsNew { get; set; }
        public string ItemTypeId { get; set; }
    }
}
