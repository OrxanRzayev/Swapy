using MediatR;
using Swapy.Common.DTO.Products.Responses;

namespace Swapy.BLL.Domain.Clothes.Queries
{
    public class GetAllClothesSeasonsQuery : IRequest<IEnumerable<SpecificationResponseDTO<string>>>
    {
    }
}
