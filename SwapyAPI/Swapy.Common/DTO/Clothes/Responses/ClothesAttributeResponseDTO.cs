using Swapy.Common.DTO.Products.Responses;

namespace Swapy.Common.DTO.Clothes.Responses
{
    public class ClothesAttributeResponseDTO : AttributeResponseDTO
    {
        public bool IsNew { get; set; }
        public string ClothesSizeId { get; set; }
        public string ClothesSize { get; set; }
        public bool IsShoe { get; set; }
        public bool IsChild { get; set; }
        public string GenderId { get; set; }
        public string Gender { get; set; }
        public string ClothesSeasonId { get; set; }
        public string ClothesSeason { get; set; }
        public string ClothesBrandId { get; set; }
        public string ClothesBrand { get; set; }
        public string ClothesViewId { get; set; }
        public string ClothesView { get; set; }
    }
}
