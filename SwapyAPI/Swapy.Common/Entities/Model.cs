namespace Swapy.Common.Entities
{
    public class Model
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string ElectronicBrandTypeId { get; set; }
        public ElectronicBrandType ElectronicBrandType { get; set; }
        public ICollection<MemoryModel> MemoriesModels { get; set; } = new List<MemoryModel>();
        public ICollection<ModelColor> ModelsColors { get; set; } = new List<ModelColor>();

        public Model() => Id = Guid.NewGuid().ToString();

        public Model(string name, string electronicBrandTypeId) : this()
        {
            Name = name;
            ElectronicBrandTypeId = electronicBrandTypeId;
        }
    }
}
