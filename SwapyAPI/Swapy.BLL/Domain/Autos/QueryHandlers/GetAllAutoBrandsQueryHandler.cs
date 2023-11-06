using MediatR;
using Swapy.BLL.Domain.Autos.Queries;
using Swapy.Common.DTO.Products.Responses;
using Swapy.DAL.Interfaces;

namespace Swapy.BLL.Domain.Autos.QueryHandlers
{
    public class GetAllAutoBrandsQueryHandler : IRequestHandler<GetAllAutoBrandsQuery, IEnumerable<SpecificationResponseDTO<string>>>
    {
        private readonly IAutoBrandRepository _autoBrandRepository;

        public GetAllAutoBrandsQueryHandler(IAutoBrandRepository autoBrandRepository) => _autoBrandRepository = autoBrandRepository;

        public async Task<IEnumerable<SpecificationResponseDTO<string>>> Handle(GetAllAutoBrandsQuery request, CancellationToken cancellationToken)
        {
            var result = (await _autoBrandRepository.GetByAutoTypesAsync(request.AutoTypesId)).Select(x => new SpecificationResponseDTO<string>(x.Id, x.Name));
            return result;
        }
    }
}
