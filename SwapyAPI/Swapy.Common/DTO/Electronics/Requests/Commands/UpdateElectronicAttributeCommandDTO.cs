using Swapy.Common.DTO.Products.Requests.Commands;

namespace Swapy.Common.DTO.Electronics.Requests.Commands
{
    public class UpdateElectronicAttributeCommandDTO : UpdateProductCommandDTO
    {
        public bool? IsNew { get; set; }
        public string? MemoryModelId { get; set; }
        public string? ModelColorId { get; set; }
    }
}
