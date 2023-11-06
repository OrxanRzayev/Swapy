using Swapy.Common.Enums;

namespace Swapy.Common.Entities
{
    public class Subcategory
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public CategoryType Type { get; set; }
        public string CategoryId { get; set; }
        public Category Category { get; set; }
        public string ParentSubcategoryId { get; set; }
        public Subcategory ParentSubcategory { get; set; }
        public SubcategoryType? SubType { get; set; }
        public ICollection<Product> Products { get; set; } = new List<Product>();
        public ICollection<AutoModel> AutoModels { get; set; } = new List<AutoModel>();
        public ICollection<AnimalBreed> AnimalBreeds { get; set; } = new List<AnimalBreed>();
        public ICollection<ClothesView> ClothesViews { get; set; } = new List<ClothesView>();
        public ICollection<ItemAttribute> ItemAttributes { get; set; } = new List<ItemAttribute>();
        public ICollection<Subcategory> ChildSubcategories { get; set; } = new List<Subcategory>();
        public ICollection<RealEstateAttribute> RealEstateAttributes { get; set; } = new List<RealEstateAttribute>();
        public ICollection<ElectronicBrandType> ElectronicBrandsTypes { get; set; } = new List<ElectronicBrandType>();

        public Subcategory() => Id = Guid.NewGuid().ToString();
             
        public Subcategory(string name, CategoryType type, string categoryId, string parentSubcategoryId, SubcategoryType subType) : this()
        {
            Name = name;
            Type = type;
            CategoryId = categoryId;
            ParentSubcategoryId = parentSubcategoryId;
            SubType = subType;
        }
    }
}