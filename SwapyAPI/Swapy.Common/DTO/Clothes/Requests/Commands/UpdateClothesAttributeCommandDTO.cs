using Swapy.Common.DTO.Products.Requests.Commands;

namespace Swapy.Common.DTO.Clothes.Requests.Commands
{
    public class UpdateClothesAttributeCommandDTO : UpdateProductCommandDTO
    {
        public bool? IsNew { get; set; }
        public string? ClothesSeasonId { get; set; }
        public string? ClothesSizeId { get; set; }
        public string? ClothesBrandViewId { get; set; }
    }
}
