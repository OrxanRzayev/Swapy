namespace Swapy.Common.Entities
{
    public class ItemAttribute
    {
        public string Id { get; set; }
        public bool IsNew { get; set; }
        public string ItemTypeId { get; set; }
        public Subcategory ItemType { get; set; }
        public string ProductId { get; set; }
        public Product Product { get; set; }

        public ItemAttribute() => Id = Guid.NewGuid().ToString();

        public ItemAttribute(bool isNew, string itemTypeId, string productId) : this()
        {
            IsNew = isNew;
            ItemTypeId = itemTypeId;
            ProductId = productId;
        }
    }
}
