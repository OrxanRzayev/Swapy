using MediatR;
using Swapy.BLL.Domain.Products.Queries;
using Swapy.Common.DTO.Products.Responses;
using Swapy.Common.Enums;
using Swapy.DAL.Interfaces;

namespace Swapy.BLL.Domain.Products.QueryHandlers
{
    public class GetProductSubcategoryQueryHandler : IRequestHandler<GetProductSubcategoryQuery, ProductSubcategoryResponseDTO>
    {
        private readonly IProductRepository _productRepository;

        public GetProductSubcategoryQueryHandler(IProductRepository productRepository, IFavoriteProductRepository favoriteProductRepository) => _productRepository = productRepository;

        public async Task<ProductSubcategoryResponseDTO> Handle(GetProductSubcategoryQuery request, CancellationToken cancellationToken)
        {
            return await _productRepository.GetProductSubcategoryAsync(request.ProductId);
        }
    }
}
