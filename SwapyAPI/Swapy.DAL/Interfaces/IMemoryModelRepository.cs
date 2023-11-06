using Swapy.Common.Entities;

namespace Swapy.DAL.Interfaces
{
    public interface IMemoryModelRepository : IRepository<MemoryModel>
    {
        Task<string> GetByMemoryAndModelAsync(string memoryId, string modelId);
    }
}
