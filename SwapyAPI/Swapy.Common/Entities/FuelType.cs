namespace Swapy.Common.Entities
{
    public class FuelType
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public ICollection<AutoAttribute> AutoAttributes { get; set; } = new List<AutoAttribute>();

        public FuelType() => Id = Guid.NewGuid().ToString();

        public FuelType(string name) : this() => Name = name;
    }
}
