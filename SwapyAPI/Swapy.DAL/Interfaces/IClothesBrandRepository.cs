using Swapy.Common.Entities;

namespace Swapy.DAL.Interfaces
{
    public interface IClothesBrandRepository : IRepository<ClothesBrand>
    {
        Task<IEnumerable<ClothesBrand>> GetByClothesViewsAsync(IEnumerable<string> clothesViewsId);
    }
}
 