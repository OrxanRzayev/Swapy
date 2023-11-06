using MediatR;
using Swapy.BLL.Domain.Animals.Queries;
using Swapy.Common.DTO.Products.Responses;
using Swapy.DAL.Interfaces;

namespace Swapy.BLL.Domain.Animals.QueryHandlers
{
    public class GetAllAnimalBreedsQueryHandler : IRequestHandler<GetAllAnimalBreedsQuery, IEnumerable<SpecificationResponseDTO<string>>>
    {
        private readonly IAnimalBreedRepository _animalBreedRepository;

        public GetAllAnimalBreedsQueryHandler(IAnimalBreedRepository animalBreedRepository) => _animalBreedRepository = animalBreedRepository;

        public async Task<IEnumerable<SpecificationResponseDTO<string>>> Handle(GetAllAnimalBreedsQuery request, CancellationToken cancellationToken)
        {
            return await _animalBreedRepository.GetByAnimalTypeAsync(request.AnimalTypesId);
        }
    }
}
