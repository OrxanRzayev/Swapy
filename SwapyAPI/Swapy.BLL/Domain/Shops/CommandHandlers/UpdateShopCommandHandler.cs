using MediatR;
using Swapy.BLL.Domain.Shops.Commands;
using Swapy.DAL.Interfaces;

namespace Swapy.BLL.Domain.Shops.CommandHandlers
{
    public class UpdateShopCommandHandler : IRequestHandler<UpdateShopCommand, Unit>
    {
        private readonly IShopAttributeRepository _shopAttributeRepository;

        public UpdateShopCommandHandler(IShopAttributeRepository shopAttributeRepository) => _shopAttributeRepository = shopAttributeRepository;

        public async Task<Unit> Handle(UpdateShopCommand request, CancellationToken cancellationToken)
        {
            var shop = await _shopAttributeRepository.GetByUserIdAsync(request.UserId);

            shop.Slogan = request.Slogan;
            shop.Location = request.Location;
            shop.Description = request.Description;
            if (!string.IsNullOrEmpty(request.ShopName)) shop.ShopName = request.ShopName;
            if (!string.IsNullOrEmpty(request.WorkDays)) shop.WorkDays = request.WorkDays;
            if (request.EndWorkTime != null) shop.EndWorkTime = request.EndWorkTime;
            if (request.StartWorkTime != null) shop.StartWorkTime = request.StartWorkTime;

            await _shopAttributeRepository.UpdateAsync(shop);

            return Unit.Value;
        }
    }
}
