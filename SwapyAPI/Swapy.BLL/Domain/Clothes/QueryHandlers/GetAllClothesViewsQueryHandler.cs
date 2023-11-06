using MediatR;
using Swapy.BLL.Domain.Clothes.Queries;
using Swapy.Common.DTO.Products.Responses;
using Swapy.DAL.Interfaces;

namespace Swapy.BLL.Domain.Clothes.QueryHandlers
{
    public class GetAllClothesViewsQueryHandler : IRequestHandler<GetAllClothesViewsQuery, IEnumerable<SpecificationResponseDTO<string>>>
    {
        private readonly IClothesViewRepository _clothesViewRepository;

        public GetAllClothesViewsQueryHandler(IClothesViewRepository clothesViewRepository) => _clothesViewRepository = clothesViewRepository;

        public async Task<IEnumerable<SpecificationResponseDTO<string>>> Handle(GetAllClothesViewsQuery request, CancellationToken cancellationToken)
        {
            return await _clothesViewRepository.GetByGenderAndTypeAsync(request.IsChild, request.GenderId, request.ClothesTypeId);
        }
    }
}
