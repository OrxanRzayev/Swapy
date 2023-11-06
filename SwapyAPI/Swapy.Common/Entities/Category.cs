using Swapy.Common.Enums;

namespace Swapy.Common.Entities
{
    public class Category
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public CategoryType Type { get; set; }
        public ICollection<Product> Products { get; set; } = new List<Product>();
        public ICollection<Subcategory> Subcategories { get; set; } = new List<Subcategory>();

        public Category() => Id = Guid.NewGuid().ToString();

        public Category(string name, CategoryType type) : this()
        {
            Name = name;
            Type = type;
        }
    }
}
