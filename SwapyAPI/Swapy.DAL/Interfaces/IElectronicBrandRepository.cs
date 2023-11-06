using Swapy.Common.Entities;

namespace Swapy.DAL.Interfaces
{
    public interface IElectronicBrandRepository : IRepository<ElectronicBrand>
    {
        Task<IEnumerable<ElectronicBrand>> GetByElectronicTypeAsync(string electronicTypeId);
    }
}
