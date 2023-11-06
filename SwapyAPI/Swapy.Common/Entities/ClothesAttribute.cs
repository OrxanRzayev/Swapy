namespace Swapy.Common.Entities
{
    public class ClothesAttribute 
    {
        public string Id { get; set; }
        public bool IsNew { get; set; } 
        public string ClothesSeasonId { get; set; }
        public ClothesSeason ClothesSeason { get; set; }
        public string ClothesSizeId { get; set; }
        public ClothesSize ClothesSize { get; set; }
        public string ClothesBrandViewId{ get; set; }
        public ClothesBrandView ClothesBrandView { get; set; }
        public string ProductId { get; set; } 
        public Product Product { get; set; }

        public ClothesAttribute() => Id = Guid.NewGuid().ToString();

        public ClothesAttribute(bool isNew, string clothesSeasonId, string clothesSizeId, string clothesBrandViewId, string productId) : this()
        { 
            IsNew = isNew;
            ClothesSeasonId = clothesSeasonId;
            ClothesSizeId = clothesSizeId;
            ClothesBrandViewId = clothesBrandViewId;
            ProductId = productId;
        }
    }
}
