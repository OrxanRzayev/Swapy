using Swapy.Common.DTO.Products.Responses;
using Swapy.Common.Entities;

namespace Swapy.DAL.Interfaces
{
    public interface IAnimalBreedRepository : IRepository<AnimalBreed>
    {
        Task<IEnumerable<SpecificationResponseDTO<string>>> GetByAnimalTypeAsync(string animalType);
    } 
}
 