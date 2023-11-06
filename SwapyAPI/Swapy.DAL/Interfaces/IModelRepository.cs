using Swapy.Common.Entities;

namespace Swapy.DAL.Interfaces
{
    public interface IModelRepository : IRepository<Model>
    {
        Task<IEnumerable<Model>> GetByBrandsAndTypeAsync(IEnumerable<string> electronicBrandsId, string electronicTypeId);
    }
}
