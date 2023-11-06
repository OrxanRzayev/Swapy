using MediatR;
using Swapy.Common.DTO.Products.Responses;

namespace Swapy.BLL.Domain.Electronics.Queries
{
    public class GetAllElectronicTypesQuery : IRequest<IEnumerable<SpecificationResponseDTO<string>>>
    {
    }
}
