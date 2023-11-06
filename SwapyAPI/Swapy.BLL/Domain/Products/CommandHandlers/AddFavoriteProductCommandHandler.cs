using MediatR;
using Swapy.BLL.Domain.Products.Commands;
using Swapy.Common.Entities;
using Swapy.DAL.Interfaces;
using Swapy.DAL.Repositories;
using System;


namespace Swapy.BLL.Domain.Products.CommandHandlers
{
    public class AddFavoriteProductCommandHandler : IRequestHandler<AddFavoriteProductCommand, FavoriteProduct>
    {
        private readonly IFavoriteProductRepository _favoriteProductRepository;
        private readonly IProductRepository _productRepository;

        public AddFavoriteProductCommandHandler(IFavoriteProductRepository favoriteProductRepository, IProductRepository productRepository)
        {
            _favoriteProductRepository = favoriteProductRepository;
            _productRepository = productRepository;
        }

        public async Task<FavoriteProduct> Handle(AddFavoriteProductCommand request, CancellationToken cancellationToken)
        {
            if((await _productRepository.GetByIdAsync(request.ProductId)).UserId == request.UserId){
                throw new ArgumentException("Invalid operation");
            }
            FavoriteProduct favoriteProduct = new FavoriteProduct(request.UserId, request.ProductId);
            await _favoriteProductRepository.CreateAsync(favoriteProduct);

            return favoriteProduct;
        }
    }
}
