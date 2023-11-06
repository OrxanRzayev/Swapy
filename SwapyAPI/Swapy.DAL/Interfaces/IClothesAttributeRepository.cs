using Swapy.Common.DTO.Products.Responses;
using Swapy.Common.Entities;

namespace Swapy.DAL.Interfaces
{
    public interface IClothesAttributeRepository : IAttributeRepository<ClothesAttribute>
    {
        Task<ClothesAttribute> GetByProductIdAsync(string productId);
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
                                                                               List<string> clothesSeasonsId,
                                                                               List<string> clothesSizesId,
                                                                               List<string> clothesBrandsId,
                                                                               List<string> clothesViewsId,
                                                                               List<string> clothesTypesId,
                                                                               List<string> clothesGendersId,
                                                                               bool? isChild,
                                                                               bool? sortByPrice,
                                                                               bool? reverseSort);
    }
}
