using MediatR;
using Swapy.BLL.Domain.Products.Queries;
using Swapy.Common.DTO.Products.Responses;
using Swapy.DAL.Interfaces;

namespace Swapy.BLL.Domain.Products.QueryHandlers
{
    public class GetAllFavoriteProductsQueryHandler : IRequestHandler<GetAllFavoriteProductsQuery, ProductsResponseDTO<ProductResponseDTO>>
    {
        private readonly IFavoriteProductRepository _favoriteProductRepository;

        public GetAllFavoriteProductsQueryHandler(IFavoriteProductRepository favoriteProductRepository) => _favoriteProductRepository = favoriteProductRepository;
        
        public async Task<ProductsResponseDTO<ProductResponseDTO>> Handle(GetAllFavoriteProductsQuery request, CancellationToken cancellationToken)
        {
            if ((request.ProductId == null) == (request.UserId == null)) throw new ArgumentException("Specify one ID for either the product or the user.");

            return await _favoriteProductRepository.GetAllFilteredAsync(request.Page,
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
                                                                        request.ProductId,
                                                                        request.SortByPrice,
                                                                        request.ReverseSort);
        }
    }
}
