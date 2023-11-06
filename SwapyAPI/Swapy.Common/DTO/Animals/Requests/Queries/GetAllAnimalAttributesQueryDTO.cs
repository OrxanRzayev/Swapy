using Swapy.Common.DTO.Products.Requests.Queries;
using Swapy.Common.Entities;

namespace Swapy.Common.DTO.Animals.Requests.Queries
{
    public class GetAllAnimalAttributesQueryDTO : GetAllBasicProductsQueryDTO<AnimalAttribute>
    {
        public List<string>? AnimalBreedsId { get; set; }
        public List<string>? AnimalTypesId { get; set; }
    }
}
