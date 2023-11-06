using Swapy.Common.Entities;

namespace Swapy.DAL.Interfaces
{
    public interface IAutoBrandRepository : IRepository<AutoBrand>
    {
        Task<IEnumerable<AutoBrand>> GetByAutoTypesAsync(IEnumerable<string> autoTypesId);
    }
}
