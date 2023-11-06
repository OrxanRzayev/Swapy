using Swapy.Common.Entities;

namespace Swapy.DAL.Interfaces
{
    public interface IClothesBrandViewRepository : IRepository<ClothesBrandView>
    {
        Task<string> GetIdByBrandAndView(string brandId, string viewId);
    }
}
