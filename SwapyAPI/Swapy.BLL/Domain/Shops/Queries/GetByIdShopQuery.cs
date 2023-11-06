using MediatR;
using Swapy.Common.DTO.Shops.Responses;

namespace Swapy.BLL.Domain.Shops.Queries
{
    public class GetByIdShopQuery : IRequest<ShopDetailResponseDTO>
    {
        public string UserId { get; set; }
        public string SenderId { get; set; }
    }
}
