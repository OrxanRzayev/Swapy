using Swapy.Common.Entities;

namespace Swapy.DAL.Interfaces
{
    public interface IMemoryRepository : IRepository<Memory>
    {
        Task<IEnumerable<Memory>> GetByModelAsync(string modelId);
    }
}
