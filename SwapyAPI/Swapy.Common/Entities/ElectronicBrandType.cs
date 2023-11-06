namespace Swapy.Common.Entities
{
    public class ElectronicBrandType
    {
        public string Id { get; set; }
        public string ElectronicBrandId { get; set; }
        public ElectronicBrand ElectronicBrand { get; set; }
        public string ElectronicTypeId { get; set; }
        public Subcategory ElectronicType { get; set; }
        public ICollection<Model> Models { get; set; } = new List<Model>();

        public ElectronicBrandType() => Id = Guid.NewGuid().ToString();

        public ElectronicBrandType(string electronicBrandId, string electronicTypeId) : this()
        {
            ElectronicBrandId = electronicBrandId;
            ElectronicTypeId = electronicTypeId;
        }
    }
}
