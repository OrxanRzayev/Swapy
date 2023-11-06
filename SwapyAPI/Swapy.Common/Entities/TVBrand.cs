namespace Swapy.Common.Entities
{
    public class TVBrand
    {
        public string Id { get; set; }
        public string Name { get; set; }  
        public ICollection<TVAttribute> TVAttributes { get; set; } = new List<TVAttribute>();

        public TVBrand() => Id = Guid.NewGuid().ToString();

        public TVBrand(string name) : this() => Name = name; 
    }
}
 