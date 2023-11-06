using MediatR;
using Swapy.BLL.Domain.Products.Queries;
using Swapy.Common.DTO.Products.Responses;
using Swapy.DAL.Interfaces;

namespace Swapy.BLL.Domain.Products.QueryHandlers
{
    public class GetAllCitiesQueryHandler : IRequestHandler<GetAllCitiesQuery, IEnumerable<SpecificationResponseDTO<string>>>
    {
        private readonly ICityRepository _cityRepository;

        public GetAllCitiesQueryHandler(ICityRepository cityRepository) => _cityRepository = cityRepository;

        public async Task<IEnumerable<SpecificationResponseDTO<string>>> Handle(GetAllCitiesQuery request, CancellationToken cancellationToken)
        {
            return await _cityRepository.GetAllSpecificationAsync();
        }
    }
}
