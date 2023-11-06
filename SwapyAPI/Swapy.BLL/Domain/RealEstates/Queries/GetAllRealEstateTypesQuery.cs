using MediatR;
using Swapy.Common.DTO.Products.Responses;

namespace Swapy.BLL.Domain.RealEstates.Queries
{
    public class GetAllRealEstateTypesQuery : IRequest<IEnumerable<SpecificationResponseDTO<string>>>
    {
    }
}
