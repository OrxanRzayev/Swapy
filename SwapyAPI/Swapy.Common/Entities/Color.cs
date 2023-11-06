namespace Swapy.Common.Entities
{
    public class Color
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public ICollection<ModelColor> ModelsColors { get; set; } = new List<ModelColor>();
        public ICollection<AutoAttribute> AutoAttributes { get; set; } = new List<AutoAttribute>();

        public Color() => Id = Guid.NewGuid().ToString();

        public Color(string name) : this() => Name = name;
    }
}
