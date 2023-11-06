using Swapy.Common.DTO.Products.Requests.Commands;

namespace Swapy.Common.DTO.RealEstates.Requests.Commands
{
    public class UpdateRealEstateAttributeCommandDTO : UpdateProductCommandDTO
    {
        public int? Area { get; set; }
        public int? Rooms { get; set; }
        public bool? IsRent { get; set; }
        public string? RealEstateTypeId { get; set; }
    }
}
