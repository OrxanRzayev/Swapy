namespace Swapy.Common.Entities
{
    public class ScreenResolution
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public ICollection<TVAttribute> TVAttributes { get; set; } = new List<TVAttribute>();

        public ScreenResolution() => Id = Guid.NewGuid().ToString();

        public ScreenResolution(string name) : this() => Name = name;
    }
}
 