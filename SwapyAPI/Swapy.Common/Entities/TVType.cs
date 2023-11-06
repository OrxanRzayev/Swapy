namespace Swapy.Common.Entities
{
    public class TVType
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public ICollection<TVAttribute> TVAttributes { get; set; } = new List<TVAttribute>(); 
        
        public TVType() => Id = Guid.NewGuid().ToString();

        public TVType(string name) : this() => Name = name;
    }
}
