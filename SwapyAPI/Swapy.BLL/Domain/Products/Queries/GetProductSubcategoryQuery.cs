using MediatR;
using Swapy.Common.DTO.Products.Responses;

namespace Swapy.BLL.Domain.Products.Queries
{
    public class GetProductSubcategoryQuery : IRequest<ProductSubcategoryResponseDTO>
    {
        public string ProductId { get; set; }
    }
}
