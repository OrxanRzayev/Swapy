using MediatR;
using Swapy.Common.DTO.Products.Responses;

namespace Swapy.BLL.Domain.Autos.Queries
{
    public class GetAllTransmissionTypesQuery : IRequest<IEnumerable<SpecificationResponseDTO<string>>>
    {
    }
}
