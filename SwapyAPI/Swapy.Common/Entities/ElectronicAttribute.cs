namespace Swapy.Common.Entities
{
    public class ElectronicAttribute
    {
        public string Id { get; set; }
        public bool IsNew { get; set; }
        public string MemoryModelId { get; set; }
        public MemoryModel MemoryModel { get; set; }
        public string ModelColorId { get; set; }
        public ModelColor ModelColor { get; set; }
        public string ProductId { get; set; }
        public Product Product { get; set; }

        public ElectronicAttribute() => Id = Guid.NewGuid().ToString();

        public ElectronicAttribute(bool isNew, string memoryModelId, string modelColorId, string productId) : this() 
        {
            IsNew = isNew;
            MemoryModelId = memoryModelId;
            ModelColorId = modelColorId;
            ProductId = productId;
        }
    }
}
