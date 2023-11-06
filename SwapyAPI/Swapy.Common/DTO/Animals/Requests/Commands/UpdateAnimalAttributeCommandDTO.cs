using Swapy.Common.DTO.Products.Requests.Commands;

namespace Swapy.Common.DTO.Animals.Requests.Commands
{
    public class UpdateAnimalAttributeCommandDTO : UpdateProductCommandDTO
    {
        public string? AnimalBreedId { get; set; }
    }
}
