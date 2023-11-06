namespace Swapy.Common.Entities
{
    public class ClothesView
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public bool IsChild { get; set; }
        public string GenderId { get; set; }
        public Gender Gender { get; set; }
        public string ClothesTypeId { get; set; }
        public Subcategory ClothesType { get; set; }
        public ICollection<ClothesBrandView> ClothesBrandViews { get; set; } = new List<ClothesBrandView>();

        public ClothesView() => Id = Guid.NewGuid().ToString();

        public ClothesView(string name, bool isChild, string genderId, string clothesTypeId) : this()
        { 
            Name = name;
            IsChild = isChild;
            GenderId = genderId;   
            ClothesTypeId = clothesTypeId;
        }  
    }
}   