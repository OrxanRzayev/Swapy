using MediatR;
using Swapy.BLL.Domain.Products.Queries;
using Swapy.Common.DTO.Products.Responses;
using Swapy.DAL.Interfaces;

namespace Swapy.BLL.Domain.Products.QueryHandlers
{
    public class GetSimilarProductsByProductIdQueryHandler : IRequestHandler<GetSimilarProductsByProductIdQuery, ProductsResponseDTO<ProductResponseDTO>>
    {
        private readonly IProductRepository _productRepository;

        public GetSimilarProductsByProductIdQueryHandler(IProductRepository productRepository) => _productRepository = productRepository;

        public async Task<ProductsResponseDTO<ProductResponseDTO>> Handle(GetSimilarProductsByProductIdQuery request, CancellationToken cancellationToken)
        {
            return await _productRepository.GetSimilarProductsById(request.Page,
                                                                request.PageSize,
                                                                request.ProductId,
                                                                request.UserId,
                                                                request.Title,
                                                                request.CurrencyId,
                                                                request.PriceMin,
                                                                request.PriceMax,
                                                                request.CategoryId,
                                                                request.SubcategoryId,
                                                                request.CityId);
        }
    }
}
