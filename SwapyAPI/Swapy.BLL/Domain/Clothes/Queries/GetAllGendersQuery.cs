using MediatR;
using Swapy.Common.DTO.Products.Responses;

namespace Swapy.BLL.Domain.Clothes.Queries
{
    public class GetAllGendersQuery : IRequest<IEnumerable<SpecificationResponseDTO<string>>>
    {
    }
}
