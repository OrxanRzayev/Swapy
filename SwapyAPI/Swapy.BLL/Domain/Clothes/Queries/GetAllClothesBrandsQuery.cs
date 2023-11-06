using MediatR;
using Swapy.Common.DTO.Products.Responses;

namespace Swapy.BLL.Domain.Clothes.Queries
{
    public class GetAllClothesBrandsQuery : IRequest<IEnumerable<SpecificationResponseDTO<string>>>
    {
        public List<string> ClothesViewsId { get; set; }
    }
}
