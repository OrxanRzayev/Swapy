using MediatR;
using Swapy.Common.DTO.Products.Responses;

namespace Swapy.BLL.Domain.TVs.Queries
{
    public class GetAllScreenResolutionsQuery : IRequest<IEnumerable<SpecificationResponseDTO<string>>>
    {
    }
}
