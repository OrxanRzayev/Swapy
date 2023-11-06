namespace Swapy.Common.Entities
{
    public class Product
    {
        public string Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }
        public DateTime DateTime { get; set; }
        public int Views { get; set; }
        public bool IsDisable { get; set; }
        public string UserId { get; set; }
        public User User { get; set; }
        public string CurrencyId { get; set; }
        public Currency Currency { get; set; }
        public string CategoryId { get; set; }
        public Category Category { get; set; }
        public string SubcategoryId { get; set; }
        public Subcategory Subcategory { get; set; }
        public string CityId { get; set; }
        public City City { get; set; }
        public string AutoAttributeId { get; set; }
        public AutoAttribute AutoAttribute { get; set; }
        public string AnimalAttributeId { get; set; }
        public AnimalAttribute AnimalAttribute { get; set; }
        public string ClothesAttributeId { get; set; }
        public ClothesAttribute ClothesAttribute { get; set; }
        public string TVAttributeId { get; set; }
        public TVAttribute TVAttribute { get; set; }
        public string RealEstateAttributeId { get; set; }
        public RealEstateAttribute RealEstateAttribute { get; set; }
        public string ElectronicAttributeId { get; set; }
        public ElectronicAttribute ElectronicAttribute { get; set; }
        public string ItemAttributeId { get; set; }
        public ItemAttribute ItemAttribute { get; set; }
        public ICollection<Chat> Chats { get; set; } = new List<Chat>();
        public ICollection<ProductImage> Images { get; set; } = new List<ProductImage>();
        public ICollection<FavoriteProduct> FavoriteProducts { get; set; } = new List<FavoriteProduct>();

        public Product() => Id = Guid.NewGuid().ToString();

        public Product(string title, string description, decimal price, string userId, string currencyId, string categoryId, string subcategoryId, string cityId) : this()
        {
            Title = title;
            Price = price;
            CityId = cityId;
            UserId = userId;
            IsDisable = false;
            CategoryId = categoryId;
            CurrencyId = currencyId;
            Description = description;
            SubcategoryId = subcategoryId;
        }

        public Product(string title, string description, decimal price, string userId, string currencyId, string categoryId, string subcategoryId, string cityId, string autoAttributeId, string animalAttributeId, string clothesAttributeId, string tVAttributeId, string realEstateAttributeId, string electronicAttributeId, string itemAttributeId) : this(title, description, price, userId, currencyId, categoryId, subcategoryId, cityId)
        {
            TVAttributeId = tVAttributeId;
            AutoAttributeId = autoAttributeId;
            ItemAttributeId = itemAttributeId;
            AnimalAttributeId = animalAttributeId;
            ClothesAttributeId = clothesAttributeId;
            ElectronicAttributeId = electronicAttributeId;
            RealEstateAttributeId = realEstateAttributeId;
        }
    }
}
