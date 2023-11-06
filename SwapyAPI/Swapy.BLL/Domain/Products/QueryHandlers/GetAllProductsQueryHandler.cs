using MediatR;
using Swapy.BLL.Domain.Products.Queries;
using Swapy.Common.DTO.Products.Responses;
using Swapy.DAL.Interfaces;

namespace Swapy.BLL.Domain.Products.QueryHandlers
{
    public class GetAllProductsQueryHandler : IRequestHandler<GetAllProductsQuery, ProductsResponseDTO<ProductResponseDTO>>
    {
        private readonly IProductRepository _productRepository;

        public GetAllProductsQueryHandler(IProductRepository productRepository, IFavoriteProductRepository favoriteProductRepository) => _productRepository = productRepository;
        
        public async Task<ProductsResponseDTO<ProductResponseDTO>> Handle(GetAllProductsQuery request, CancellationToken cancellationToken)
        {
            return await _productRepository.GetAllFilteredAsync(request.Page,
                                                                request.PageSize,
                                                                request.UserId,
                                                                request.Title,
                                                                request.CurrencyId,
                                                                request.PriceMin,
                                                                request.PriceMax,
                                                                request.CategoryId,
                                                                request.SubcategoryId,
                                                                request.CityId,
                                                                request.OtherUserId,
                                                                request.IsDisable,
                                                                request.SortByPrice,
                                                                request.ReverseSort);
        }
    }
}
