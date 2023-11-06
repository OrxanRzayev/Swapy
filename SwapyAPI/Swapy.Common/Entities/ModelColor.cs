namespace Swapy.Common.Entities
{
    public class ModelColor
    {
        public string Id { get; set; }
        public string ModelId { get; set; }
        public Model Model { get; set; }
        public string ColorId { get; set; }
        public Color Color { get; set; }
        public ICollection<ElectronicAttribute> ElectronicAttributes { get; set; } = new List<ElectronicAttribute>();

        public ModelColor() => Id = Guid.NewGuid().ToString();

        public ModelColor(string id, string modelId, string colorId) : this()
        {
            Id = id;
            ModelId = modelId;
            ColorId = colorId;
        }
    }
}
