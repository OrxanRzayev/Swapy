using MediatR;
using Swapy.BLL.Domain.Electronics.Queries;
using Swapy.Common.DTO.Products.Responses;
using Swapy.DAL.Interfaces;

namespace Swapy.BLL.Domain.Electronics.QueryHandlers
{
    public class GetAllElectronicBrandsQueryHandler : IRequestHandler<GetAllElectronicBrandsQuery, IEnumerable<SpecificationResponseDTO<string>>>
    {
        private readonly IElectronicBrandRepository _electronicBrandRepository;

        public GetAllElectronicBrandsQueryHandler(IElectronicBrandRepository electronicBrandRepository) => _electronicBrandRepository = electronicBrandRepository;

        public async Task<IEnumerable<SpecificationResponseDTO<string>>> Handle(GetAllElectronicBrandsQuery request, CancellationToken cancellationToken)
        {
            var result = (await _electronicBrandRepository.GetByElectronicTypeAsync(request.ElectronicTypeId)).Select(x => new SpecificationResponseDTO<string>(x.Id, x.Name));
            return result;
        }
    }
}
