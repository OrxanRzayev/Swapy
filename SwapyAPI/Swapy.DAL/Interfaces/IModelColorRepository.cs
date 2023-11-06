using Swapy.Common.Entities;

namespace Swapy.DAL.Interfaces
{
    public interface IModelColorRepository : IRepository<ModelColor>
    {
        Task<string> GetByModelAndColorAsync(string modelId, string colorId);
    }
}
