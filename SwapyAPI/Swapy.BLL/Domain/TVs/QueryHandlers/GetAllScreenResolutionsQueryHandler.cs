using MediatR;
using Swapy.BLL.Domain.TVs.Queries;
using Swapy.Common.DTO.Products.Responses;
using Swapy.DAL.Interfaces;

namespace Swapy.BLL.Domain.TVs.QueryHandlers
{
    public class GetAllScreenResolutionsQueryHandler : IRequestHandler<GetAllScreenResolutionsQuery, IEnumerable<SpecificationResponseDTO<string>>>
    {
        private readonly IScreenResolutionRepository _screenResolutionRepository;

        public GetAllScreenResolutionsQueryHandler(IScreenResolutionRepository screenResolutionRepository) => _screenResolutionRepository = screenResolutionRepository;

        public async Task<IEnumerable<SpecificationResponseDTO<string>>> Handle(GetAllScreenResolutionsQuery request, CancellationToken cancellationToken)
        {
            var result = (await _screenResolutionRepository.GetAllAsync()).Select(x => new SpecificationResponseDTO<string>(x.Id, x.Name));
            return result;
        }
    }
}
