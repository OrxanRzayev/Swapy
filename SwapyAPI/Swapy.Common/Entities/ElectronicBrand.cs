namespace Swapy.Common.Entities
{
    public class ElectronicBrand
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public ICollection<ElectronicBrandType> ElectronicBrandsTypes { get; set; } = new List<ElectronicBrandType>();

        public ElectronicBrand() => Id = Guid.NewGuid().ToString();

        public ElectronicBrand(string name) : this() => Name = name;
    }
}
