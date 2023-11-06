using MediatR;
using Swapy.BLL.Domain.Animals.Queries;
using Swapy.Common.DTO.Products.Responses;
using Swapy.DAL.Interfaces;

namespace Swapy.BLL.Domain.Animals.QueryHandlers
{
    public class GetAllAnimalAttributesQueryHandler : IRequestHandler<GetAllAnimalAttributesQuery, ProductsResponseDTO<ProductResponseDTO>>
    {
        private readonly IAnimalAttributeRepository _animalAttributeRepository;

        public GetAllAnimalAttributesQueryHandler(IAnimalAttributeRepository animalAttributeRepository) => _animalAttributeRepository = animalAttributeRepository;

        public async Task<ProductsResponseDTO<ProductResponseDTO>> Handle(GetAllAnimalAttributesQuery request, CancellationToken cancellationToken)
        {
            return await _animalAttributeRepository.GetAllFilteredAsync(request.Page,
                                                                        request.PageSize,
                                                                        request.UserId,
                                                                        request.Title,
                                                                        request.CurrencyId,
                                                                        request.PriceMin,
                                                                        request.PriceMax,
                                                                        request.CategoryId,
                                                                        request.SubcategoryId,
                                                                        request.CityId,
                                                                        request.OtherUserId,
                                                                        request.AnimalBreedsId,
                                                                        request.AnimalTypesId,
                                                                        request.SortByPrice,
                                                                        request.ReverseSort);
        }
    }
}
