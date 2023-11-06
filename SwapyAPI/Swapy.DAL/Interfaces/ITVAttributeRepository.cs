using Swapy.Common.DTO.Products.Responses;
using Swapy.Common.Entities;

namespace Swapy.DAL.Interfaces
{
    public interface ITVAttributeRepository : IAttributeRepository<TVAttribute>
    {
        Task<TVAttribute> GetByProductIdAsync(string productId);
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
                                                                          bool? isNew,
                                                                          bool? isSmart,
                                                                          List<string> tvTypesId,
                                                                          List<string> tvBrandsId,
                                                                          List<string> screenResolutionsId,
                                                                          List<string> screenDiagonalsId,
                                                                          bool? sortByPrice,
                                                                          bool? reverseSort);
    }
}
