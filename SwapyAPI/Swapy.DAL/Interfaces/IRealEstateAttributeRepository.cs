using Swapy.Common.DTO.RealEstates.Responses;
using Swapy.Common.Entities;

namespace Swapy.DAL.Interfaces
{
    public interface IRealEstateAttributeRepository : IAttributeRepository<RealEstateAttribute>
    {
        Task<RealEstateAttribute> GetByProductIdAsync(string productId);
        Task<RealEstateAttributesResponseDTO> GetAllFilteredAsync(int page,
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
                                                                          bool? isRent,
                                                                          int? areaMax,
                                                                          int? areaMin,
                                                                          int? roomsMin,
                                                                          int? roomsMax,
                                                                          List<string> realEstateTypesId,
                                                                          bool? sortByPrice,
                                                                          bool? reverseSort);
    }
}
