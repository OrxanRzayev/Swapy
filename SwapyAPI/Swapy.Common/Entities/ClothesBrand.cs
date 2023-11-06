namespace Swapy.Common.Entities
{
    public class ClothesBrand
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public ICollection<ClothesBrandView> ClothesBrandsViews { get; set; } = new List<ClothesBrandView>();

        public ClothesBrand() => Id = Guid.NewGuid().ToString();

        public ClothesBrand(string name) : this() => Name = name;
    } 
}
 