using Swapy.BLL.Domain.Products.Commands;
using Swapy.Common.Entities;

namespace Swapy.BLL.Domain.Clothes.Commands
{
    public class AddClothesAttributeCommand : AddProductCommand<ClothesAttribute>
    {
        public bool IsNew { get; set; }
        public string ClothesSeasonId { get; set; }
        public string ClothesSizeId { get; set; }
        public string ClothesBrandViewId { get; set; }
    }
}
