using Swapy.Common.DTO.Products.Responses;

namespace Swapy.Common.DTO.Animals.Responses
{
    public class AnimalAttributeResponseDTO : AttributeResponseDTO
    {
        public string BreedId { get; set; }
        public string Breed { get; set; }
    }
}
