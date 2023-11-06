using Swapy.Common.DTO.Products.Responses;

namespace Swapy.Common.DTO.Electronics.Responses
{
    public class ElectronicAttributeResponseDTO : AttributeResponseDTO
    {
        public bool IsNew { get; set; }
        public string ColorId { get; set; }
        public string Color { get; set; }
        public string MemoryId { get; set; }
        public string Memory { get; set; }
        public string ModelId { get; set; }
        public string Model { get; set; }
        public string ElectronicBrandId { get; set; }
        public string ElectronicBrand { get; set; }
    }
}
