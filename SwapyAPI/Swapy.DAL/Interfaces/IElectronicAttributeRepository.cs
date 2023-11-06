using Swapy.Common.DTO.Products.Responses;
using Swapy.Common.Entities;

namespace Swapy.DAL.Interfaces
{
    public interface IElectronicAttributeRepository : IAttributeRepository<ElectronicAttribute>
    {
        Task<ElectronicAttribute> GetByProductIdAsync(string productId);
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
                                                                               List<string> memoriesId,
                                                                               List<string> colorsId,
                                                                               List<string> modelsId,
                                                                               List<string> brandsId,
                                                                               List<string> typesId,
                                                                               bool? sortByPrice,
                                                                               bool? reverseSort);
    }
}
