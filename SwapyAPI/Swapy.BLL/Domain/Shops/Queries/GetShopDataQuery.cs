using MediatR;
using Swapy.Common.DTO.Shops.Responses;

namespace Swapy.BLL.Domain.Shops.Queries
{
    public class GetShopDataQuery : IRequest<ShopDataResponseDTO>
    {
        public string UserId { get; set; }
    }
}
