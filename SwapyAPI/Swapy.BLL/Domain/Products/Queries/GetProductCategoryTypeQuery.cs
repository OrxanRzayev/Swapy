using MediatR;
using Swapy.Common.DTO.Products.Responses;
using Swapy.Common.Enums;

namespace Swapy.BLL.Domain.Products.Queries
{
    public class GetProductCategoryTypeQuery : IRequest<SpecificationResponseDTO<CategoryType>>
    {
        public string? ProductId { get; set; }
    }
}
