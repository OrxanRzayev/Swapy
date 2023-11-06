namespace Swapy.Common.Entities
{
    public class ClothesBrandView
    {
        public string Id { get; set; }
        public string ClothesBrandId { get; set; }
        public ClothesBrand ClothesBrand { get; set; }
        public string ClothesViewId { get; set; }
        public ClothesView ClothesView { get; set; }

        public ICollection<ClothesAttribute> ClothesAttributes { get; set; } = new List<ClothesAttribute>();

        public ClothesBrandView() => Id = Guid.NewGuid().ToString();

        public ClothesBrandView(string clothesBrandId, string clothesViewId) : this()
        {  
            ClothesBrandId = clothesBrandId;
            ClothesViewId = clothesViewId;
        } 
    }

} 