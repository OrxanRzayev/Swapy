using Swapy.BLL.Domain.Products.Commands;

namespace Swapy.BLL.Domain.Electronics.Commands
{
    public class UpdateElectronicAttributeCommand : UpdateProductCommand
    {
        public bool? IsNew { get; set; }
        public string MemoryModelId { get; set; }
        public string ModelColorId { get; set; }
    }
}
