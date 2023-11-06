using Swapy.Common.DTO.Products.Requests.Commands;
using Swapy.Common.Entities;

namespace Swapy.Common.DTO.Animals.Requests.Commands
{
    public class AddAnimalAttributeCommandDTO : AddProductCommandDTO
    {
        public string AnimalBreedId { get; set; }
    }
}
