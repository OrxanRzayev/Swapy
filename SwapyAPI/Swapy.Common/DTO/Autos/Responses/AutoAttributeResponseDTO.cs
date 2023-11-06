using Swapy.Common.DTO.Products.Responses;

namespace Swapy.Common.DTO.Autos.Responses
{
    public class AutoAttributeResponseDTO : AttributeResponseDTO
    {
        public bool IsNew { get; set; }
        public int Miliage { get; set; }
        public int EngineCapacity { get; set; }
        public DateTime ReleaseYear { get; set; }
        public string FuelTypeId { get; set; }
        public string FuelType { get; set; }
        public string ColorId { get; set; }
        public string Color { get; set; }
        public string TransmissionTypeId { get; set; }
        public string TransmissionType { get; set; }
        public string AutoBrandId { get; set; }
        public string AutoBrand { get; set; }
        public string AutoModelId { get; set; }
        public string AutoModel { get; set; }
    }
}
