using MediatR;
using Swapy.BLL.Domain.Clothes.Queries;
using Swapy.Common.DTO.Products.Responses;
using Swapy.DAL.Interfaces;

namespace Swapy.BLL.Domain.Clothes.QueryHandlers
{
    public class GetAllClothesSeasonsQueryHandler : IRequestHandler<GetAllClothesSeasonsQuery, IEnumerable<SpecificationResponseDTO<string>>>
    {
        private readonly IClothesSeasonRepository _clothesSeasonRepository;

        public GetAllClothesSeasonsQueryHandler(IClothesSeasonRepository clothesSeasonRepository) => _clothesSeasonRepository = clothesSeasonRepository;

        public async Task<IEnumerable<SpecificationResponseDTO<string>>> Handle(GetAllClothesSeasonsQuery request, CancellationToken cancellationToken)
        {
            return await _clothesSeasonRepository.GetAllSpecificationAsync();
        }
    }
}
