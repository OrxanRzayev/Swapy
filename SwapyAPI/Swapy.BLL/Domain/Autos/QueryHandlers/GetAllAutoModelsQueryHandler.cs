using MediatR;
using Swapy.BLL.Domain.Autos.Queries;
using Swapy.Common.DTO.Products.Responses;
using Swapy.DAL.Interfaces;

namespace Swapy.BLL.Domain.Autos.QueryHandlers
{
    public class GetAllAutoModelsQueryHandler : IRequestHandler<GetAllAutoModelsQuery, IEnumerable<SpecificationResponseDTO<string>>>
    {
        private readonly IAutoModelRepository _autoModelRepository;

        public GetAllAutoModelsQueryHandler(IAutoModelRepository autoModelRepository) => _autoModelRepository = autoModelRepository;

        public async Task<IEnumerable<SpecificationResponseDTO<string>>> Handle(GetAllAutoModelsQuery request, CancellationToken cancellationToken)
        {
            var result = (await _autoModelRepository.GetByBrandsAndTypesAsync(request.AutoBrandsId, request.AutoTypesId)).Select(x => new SpecificationResponseDTO<string>(x.Id, x.Name));
            return result;
        }
    }
}
