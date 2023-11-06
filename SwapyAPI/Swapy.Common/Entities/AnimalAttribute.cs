namespace Swapy.Common.Entities
{
    public class AnimalAttribute
    {
        public string Id { get; set; } 
        public string AnimalBreedId { get; set; } 
        public AnimalBreed AnimalBreed { get; set; } 
        public string ProductId { get; set; } 
        public Product Product { get; set; }

        public AnimalAttribute() => Id = Guid.NewGuid().ToString();

        public AnimalAttribute(string animalBreedId, string productId) : this()
        { 
            ProductId = productId;
            AnimalBreedId = animalBreedId;
        }
    } 
}
