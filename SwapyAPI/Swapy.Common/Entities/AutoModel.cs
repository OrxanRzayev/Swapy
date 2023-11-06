namespace Swapy.Common.Entities
{
    public class AutoModel
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string AutoBrandId { get; set; }
        public AutoBrand AutoBrand { get; set; }
        public string AutoTypeId { get; set; }
        public Subcategory AutoType { get; set; }
        public ICollection<AutoAttribute> AutoAttributes { get; set; } = new List<AutoAttribute>();

        public AutoModel() => Id = Guid.NewGuid().ToString();

        public AutoModel(string name, string autoBrandId, string autoTypeId) : this()
        {
            Name = name;
            AutoBrandId = autoBrandId;
            AutoTypeId = autoTypeId;
        }
    }
}
