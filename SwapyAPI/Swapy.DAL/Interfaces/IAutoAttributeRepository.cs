using Swapy.Common.DTO.Autos.Responses;
using Swapy.Common.Entities;

namespace Swapy.DAL.Interfaces
{
    public interface IAutoAttributeRepository : IAttributeRepository<AutoAttribute>
    {
        Task<AutoAttribute> GetByProductIdAsync(string productId);
        Task<AutoAttributesResponseDTO> GetAllFilteredAsync(int page,
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
                                                                               int? miliageMin,
                                                                               int? miliageMax,
                                                                               int? engineCapacityMin,
                                                                               int? engineCapacityMax,
                                                                               DateTime? releaseYearOlder,
                                                                               DateTime? releaseYearNewer,
                                                                               List<string> fuelTypesId,
                                                                               List<string> autoColorsId,
                                                                               List<string> transmissionTypesId,
                                                                               List<string> autoBrandsId,
                                                                               List<string> autoTypesId,
                                                                               bool? sortByPrice,
                                                                               bool? reverseSort);
    }
}
