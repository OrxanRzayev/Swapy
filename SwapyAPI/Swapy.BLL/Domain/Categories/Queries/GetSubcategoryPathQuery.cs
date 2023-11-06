using MediatR;
using Swapy.Common.DTO.Products.Responses;

namespace Swapy.BLL.Domain.Categories.Queries
{
    public class GetSubcategoryPathQuery : IRequest<IEnumerable<SpecificationResponseDTO<string>>>
    {
        public string SubcategoryId { get; set; }
    }
}
