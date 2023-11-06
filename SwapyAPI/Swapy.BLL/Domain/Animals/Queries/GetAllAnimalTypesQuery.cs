using MediatR;
using Swapy.Common.DTO.Products.Responses;

namespace Swapy.BLL.Domain.Animals.Queries
{
    public class GetAllAnimalTypesQuery : IRequest<IEnumerable<SpecificationResponseDTO<string>>>
    {
    }
}
