using Swapy.Common.DTO.Products.Requests.Commands;
using Swapy.Common.Entities;

namespace Swapy.Common.DTO.Electronics.Requests.Commands
{
    public class AddElectronicAttributeCommandDTO : AddProductCommandDTO
    {
        public bool IsNew { get; set; }
        public string MemoryModelId { get; set; }
        public string ModelColorId { get; set; }
    }
}
