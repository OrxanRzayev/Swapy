using Swapy.Common.DTO.Products.Responses;

namespace Swapy.Common.DTO.RealEstates.Responses
{
    public class RealEstateAttributeResponseDTO : AttributeResponseDTO
    {
        public int Area { get; set; }
        public int Rooms { get; set; }
        public bool IsRent { get; set; }
    }
}
