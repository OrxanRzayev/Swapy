namespace Swapy.Common.Entities
{
    public class TransmissionType
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public ICollection<AutoAttribute> AutoAttributes { get; set; } = new List<AutoAttribute>();

        public TransmissionType() => Id = Guid.NewGuid().ToString();

        public TransmissionType(string name) : this() => Name = name;
    }
}
