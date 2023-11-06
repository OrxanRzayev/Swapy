using Swapy.Common.DTO.Shops.Responses;
using Swapy.Common.Entities;

namespace Swapy.DAL.Interfaces
{
    public interface IShopAttributeRepository : IAttributeRepository<ShopAttribute>
    {
        Task IncrementViewsAsync(string shopId);
        Task<ShopAttribute> GetByUserIdAsync(string userId);
        Task<bool> FindByShopNameAsync(string shopName);
        Task<ShopsResponseDTO> GetAllFilteredAsync(int page, int pageSize, string title, bool? sortByViews, bool? reverseSort);
    }
}
