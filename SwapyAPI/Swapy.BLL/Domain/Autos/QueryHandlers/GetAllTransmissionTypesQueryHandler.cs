using MediatR;
using Swapy.BLL.Domain.Autos.Queries;
using Swapy.Common.DTO.Products.Responses;
using Swapy.DAL.Interfaces;

namespace Swapy.BLL.Domain.Autos.QueryHandlers
{
    public class GetAllTransmissionTypesQueryHandler : IRequestHandler<GetAllTransmissionTypesQuery, IEnumerable<SpecificationResponseDTO<string>>>
    {
        private readonly ITransmissionTypeRepository _transmissionTypeRepository;

        public GetAllTransmissionTypesQueryHandler(ITransmissionTypeRepository transmissionTypeRepository) => _transmissionTypeRepository = transmissionTypeRepository;

        public async Task<IEnumerable<SpecificationResponseDTO<string>>> Handle(GetAllTransmissionTypesQuery request, CancellationToken cancellationToken)
        {
            return await _transmissionTypeRepository.GetAllSpecificationAsync();
        }
    }
}
