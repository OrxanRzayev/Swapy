using Swapy.Common.DTO.Products.Requests.Commands;
using Swapy.Common.Entities;

namespace Swapy.Common.DTO.Clothes.Requests.Commands
{
    public class AddClothesAttributeCommandDTO : AddProductCommandDTO
    {
        public bool IsNew { get; set; }
        public string ClothesSeasonId { get; set; }
        public string ClothesSizeId { get; set; }
        public string ClothesBrandViewId { get; set; }
    }
}
