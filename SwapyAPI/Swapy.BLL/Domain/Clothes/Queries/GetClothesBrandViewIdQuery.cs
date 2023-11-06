using MediatR;
using Swapy.Common.DTO.Autos.Responses;

namespace Swapy.BLL.Domain.Clothes.Queries
{
    public class GetClothesBrandViewIdQuery : IRequest<string>
    {
        public string BrandId { get; set; }
        public string ClothesViewId { get; set; }
    }
}
