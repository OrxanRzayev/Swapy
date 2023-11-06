namespace Swapy.Common.Entities
{
    public class AnimalBreed
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string AnimalTypeId { get; set; } 
        public Subcategory AnimalType { get; set; } 
        public ICollection<AnimalAttribute> AnimalAttributes { get; set; } = new List<AnimalAttribute>();

        public AnimalBreed() => Id = Guid.NewGuid().ToString();

        public AnimalBreed(string name, string animalTypeId) : this()
        {
            Name = name;
            AnimalTypeId = animalTypeId;
        }
    } 
}  