namespace Swapy.Common.Entities
{
    public class MemoryModel
    {
        public string Id { get; set; }
        public string ModelId { get; set; }
        public Model Model { get; set; }
        public string MemoryId { get; set; }
        public Memory Memory { get; set; }
        public ICollection<ElectronicAttribute> ElectronicAttributes { get; set; } = new List<ElectronicAttribute>();

        public MemoryModel() => Id = Guid.NewGuid().ToString();

        public MemoryModel(string modelId, string memoryId) : this()
        {
            ModelId = modelId;
            MemoryId = memoryId;
        }
    }
}
