using Swapy.Common.Entities;

namespace Swapy.DAL.Interfaces
{
    public interface IAutoModelRepository : IRepository<AutoModel>
    {
        Task<IEnumerable<AutoModel>> GetByBrandsAndTypesAsync(IEnumerable<string> autoBrandsId, IEnumerable<string> autoTypesId);
    }
}
