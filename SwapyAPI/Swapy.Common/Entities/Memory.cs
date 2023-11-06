namespace Swapy.Common.Entities
{
    public class Memory
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public ICollection<MemoryModel> MemoriesModels { get; set; } = new List<MemoryModel>();

        public Memory() => Id = Guid.NewGuid().ToString();

        public Memory(string name) : this() => Name = name;
    }
}
