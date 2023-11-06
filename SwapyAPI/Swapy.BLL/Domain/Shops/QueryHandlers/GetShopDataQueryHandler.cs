using MediatR;
using Microsoft.AspNetCore.Identity;
using Swapy.BLL.Domain.Shops.Queries;
using Swapy.Common.DTO.Shops.Responses;
using Swapy.Common.DTO.Users.Responses;
using Swapy.Common.Entities;
using Swapy.DAL.Interfaces;

namespace Swapy.BLL.Domain.Shops.QueryHandlers
{
    public class GetShopDataQueryHandler : IRequestHandler<GetShopDataQuery, ShopDataResponseDTO>
    {
        private readonly IShopAttributeRepository _shopAttributeRepository;

        public GetShopDataQueryHandler(IShopAttributeRepository shopAttributeRepository) => _shopAttributeRepository = shopAttributeRepository;

        public async Task<ShopDataResponseDTO> Handle(GetShopDataQuery request, CancellationToken cancellationToken)
        {
            var shop = await _shopAttributeRepository.GetByUserIdAsync(request.UserId);

            return new ShopDataResponseDTO()
            {
                Banner = shop.Banner,
                Slogan = shop.Slogan,
                Logo = shop.User.Logo,
                Location = shop.Location,
                ShopName = shop.ShopName,
                WorkDays = shop.WorkDays,
                Description = shop.Description,
                EndWorkTime = shop.EndWorkTime,
                StartWorkTime = shop.StartWorkTime,
                PhoneNumber = shop.User.PhoneNumber,
            };
        }
    }
}
