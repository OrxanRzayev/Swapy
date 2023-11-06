using Swapy.BLL.Domain.Products.Commands;

namespace Swapy.BLL.Domain.Animals.Commands
{
    public class UpdateAnimalAttributeCommand : UpdateProductCommand
    {
        public string AnimalBreedId { get; set; }
    }
}
