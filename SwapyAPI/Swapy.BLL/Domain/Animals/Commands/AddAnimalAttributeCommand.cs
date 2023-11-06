using Swapy.BLL.Domain.Products.Commands;
using Swapy.Common.Entities;

namespace Swapy.BLL.Domain.Animals.Commands
{
    public class AddAnimalAttributeCommand : AddProductCommand<AnimalAttribute>
    {
        public string AnimalBreedId { get; set; }
    }
}
