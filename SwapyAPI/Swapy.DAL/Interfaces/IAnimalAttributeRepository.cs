using Swapy.Common.DTO.Products.Responses;
using Swapy.Common.Entities;

namespace Swapy.DAL.Interfaces
{
    public interface IAnimalAttributeRepository : IAttributeRepository<AnimalAttribute>
    {
        Task<AnimalAttribute> GetByProductIdAsync(string productId);
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
                                                                          List<string> animalBreedsId,
                                                                          List<string> animalTypesId,
                                                                          bool? sortByPrice,
                                                                          bool? reverseSort);
    }
}
 