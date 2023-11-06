using MediatR;
using Swapy.BLL.Domain.Auth.Commands;
using Swapy.DAL.Interfaces;

namespace Swapy.BLL.Domain.Auth.CommandHandlers
{
    public class CheckShopNameCommandHandler : IRequestHandler<ShopNameCommand, bool>
    {
        private readonly IShopAttributeRepository _shopAttributeRepository;

        public CheckShopNameCommandHandler(IShopAttributeRepository shopAttributeRepository) => _shopAttributeRepository = shopAttributeRepository;

        public async Task<bool> Handle(ShopNameCommand request, CancellationToken cancellationToken)
        {
            return await _shopAttributeRepository.FindByShopNameAsync(request.ShopName);
        }
    }
}
