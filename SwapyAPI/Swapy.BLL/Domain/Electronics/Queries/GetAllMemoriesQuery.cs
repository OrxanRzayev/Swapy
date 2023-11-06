using MediatR;
using Swapy.Common.DTO.Products.Responses;

namespace Swapy.BLL.Domain.Electronics.Queries
{
    public class GetAllMemoriesQuery : IRequest<IEnumerable<SpecificationResponseDTO<string>>>
    {
        public string ModelId { get; set; }
    }
}
