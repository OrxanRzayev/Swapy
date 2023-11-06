using MediatR;
using Swapy.Common.DTO.Products.Responses;

namespace Swapy.BLL.Domain.Items.Queries
{
    public class GetAllItemSectionsQuery : IRequest<IEnumerable<SpecificationResponseDTO<string>>>
    {
    }
}
