using Swapy.Common.DTO.Products.Requests.Commands;
using Swapy.Common.Entities;

namespace Swapy.Common.DTO.RealEstates.Requests.Commands
{
    public class AddRealEstateAttributeCommandDTO : AddProductCommandDTO
    {
        public int Area { get; set; }
        public int Rooms { get; set; }
        public bool IsRent { get; set; }
        public string RealEstateTypeId { get; set; }
    }
}
