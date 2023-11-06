using MediatR;
using Swapy.BLL.Domain.Products.Commands;
using Swapy.Common.Exceptions;
using Swapy.DAL.Interfaces;

namespace Swapy.BLL.Domain.Products.CommandHandlers
{
    public class RemoveFavoriteProductCommandHandler : IRequestHandler<RemoveFavoriteProductCommand, Unit>
    {
        private readonly IFavoriteProductRepository _favoriteProductRepository;

        public RemoveFavoriteProductCommandHandler(IFavoriteProductRepository favoriteProductRepository)
        {
            _favoriteProductRepository = favoriteProductRepository;
        }

        public async Task<Unit> Handle(RemoveFavoriteProductCommand request, CancellationToken cancellationToken)
        {
            var favoriteProduct = await _favoriteProductRepository.GetByProductAndUserIdAsync(request.ProductId, request.UserId);

            if (!request.UserId.Equals(favoriteProduct.UserId)) throw new NoAccessException("No access to delete this product");

            await _favoriteProductRepository.DeleteAsync(favoriteProduct);

            return Unit.Value;
        }
    }
}
