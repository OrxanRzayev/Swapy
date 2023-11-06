using MediatR;
using Swapy.BLL.Domain.Products.Commands;
using Swapy.Common.Exceptions;
using Swapy.DAL.Interfaces;

namespace Swapy.BLL.Domain.Products.CommandHandlers
{
    public class SwitchProductEnablingCommandHandler : IRequestHandler<SwitchProductEnablingCommand, Unit>
    {
        private readonly IProductRepository _productRepository;
        private readonly IFavoriteProductRepository _favoriteProductRepository;

        public SwitchProductEnablingCommandHandler(IProductRepository productRepository, IFavoriteProductRepository favoriteProductRepository)
        {
            _productRepository = productRepository;
            _favoriteProductRepository = favoriteProductRepository;
        }

        public async Task<Unit> Handle(SwitchProductEnablingCommand request, CancellationToken cancellationToken)
        {
            var product = await _productRepository.GetByIdAsync(request.ProductId);

            if (!request.UserId.Equals(product.UserId)) throw new NoAccessException("No access to change enabling this product");

            product.IsDisable = !product.IsDisable;
            await _productRepository.UpdateAsync(product);

            if(product.IsDisable)
            {
                await _favoriteProductRepository.RemoveFavoriteByProductId(product.Id);
            }

            return Unit.Value;
        }
    }
}
