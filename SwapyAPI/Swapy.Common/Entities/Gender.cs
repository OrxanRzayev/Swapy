namespace Swapy.Common.Entities
{
    public class Gender
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public ICollection<ClothesView> ClothesViews { get; set; } = new List<ClothesView>();

        public Gender() => Id = Guid.NewGuid().ToString();

        public Gender(string name) : this() => Name = name;
    }
}
 