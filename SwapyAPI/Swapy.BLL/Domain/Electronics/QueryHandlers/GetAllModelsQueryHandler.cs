using MediatR;
using Swapy.BLL.Domain.Electronics.Queries;
using Swapy.Common.DTO.Products.Responses;
using Swapy.DAL.Interfaces;

namespace Swapy.BLL.Domain.Electronics.QueryHandlers
{
    public class GetAllModelsQueryHandler : IRequestHandler<GetAllModelsQuery, IEnumerable<SpecificationResponseDTO<string>>>
    {
        private readonly IModelRepository _modelRepository;

        public GetAllModelsQueryHandler(IModelRepository modelRepository) => _modelRepository = modelRepository;

        public async Task<IEnumerable<SpecificationResponseDTO<string>>> Handle(GetAllModelsQuery request, CancellationToken cancellationToken)
        {
            var result = (await _modelRepository.GetByBrandsAndTypeAsync(request.ElectronicBrandsId, request.ElectronicTypeId)).Select(x => new SpecificationResponseDTO<string>(x.Id, x.Name));
            return result;
        }
    }
}
