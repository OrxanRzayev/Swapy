using Swapy.BLL.Domain.Products.Commands;

namespace Swapy.BLL.Domain.Clothes.Commands
{
    public class UpdateClothesAttributeCommand : UpdateProductCommand
    {
        public bool? IsNew { get; set; }
        public string ClothesSeasonId { get; set; }
        public string ClothesSizeId { get; set; }
        public string ClothesBrandViewId { get; set; }
    }
}
