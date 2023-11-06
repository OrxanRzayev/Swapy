using Swapy.Common.DTO.Products.Responses;
using Swapy.Common.Entities;

namespace Swapy.DAL.Interfaces
{
    public interface IFavoriteProductRepository : IAttributeRepository<FavoriteProduct>
    {
        Task<FavoriteProduct> GetByProductAndUserIdAsync(string productId, string userId);
        Task<bool> CheckProductOnFavorite(string productId, string userId);
        Task<ProductsResponseDTO<ProductResponseDTO>> GetAllFilteredAsync(int page,
                                                                          int pageSize,
                                                                          string userId,
                                                                          string title,
                                                                          string currencyId,
                                                                          decimal? priceMin,
                                                                          decimal? priceMax,
                                                                          string categoryId,
                                                                          string subcategoryId,
                                                                          string cityId,
                                                                          string otherUserId,
                                                                          string productId,
                                                                          bool? sortByPrice,
                                                                          bool? reverseSort);

        Task RemoveFavoriteByProductId(string productId);
    }
}
