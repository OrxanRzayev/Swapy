namespace Swapy.Common.Entities
{
    public class AutoBrand
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public ICollection<AutoModel> AutoModels { get; set; } = new List<AutoModel>();

        public AutoBrand() => Id = Guid.NewGuid().ToString();

        public AutoBrand(string name) : this() => Name = name;
    }
}
