using MediatR;
using Swapy.BLL.Domain.TVs.Queries;
using Swapy.Common.DTO.Products.Responses;
using Swapy.DAL.Interfaces;

namespace Swapy.BLL.Domain.TVs.QueryHandlers
{
    public class GetAllTVTypesQueryHandler : IRequestHandler<GetAllTVTypesQuery, IEnumerable<SpecificationResponseDTO<string>>>
    {
        private readonly ITVTypeRepository _tvTypeRepository;

        public GetAllTVTypesQueryHandler(ITVTypeRepository tvTypeRepository) => _tvTypeRepository = tvTypeRepository;

        public async Task<IEnumerable<SpecificationResponseDTO<string>>> Handle(GetAllTVTypesQuery request, CancellationToken cancellationToken)
        {
            return await _tvTypeRepository.GetAllSpecificationAsync();
        }
    }
}
