using Swapy.Common.Entities;

namespace Swapy.DAL.Interfaces
{
    public interface IClothesSizeRepository : IRepository<ClothesSize>
    {
        Task<IEnumerable<ClothesSize>> GetByChildAndShoeAsync(bool isChild, bool isShoe);
    }
}
