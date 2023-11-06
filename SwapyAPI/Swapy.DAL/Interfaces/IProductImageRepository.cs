using Swapy.Common.Entities;

namespace Swapy.DAL.Interfaces
{
    public interface IProductImageRepository : IRepository<ProductImage>
    {
        Task<ProductImage> GetByPath(string path, string productId);
        Task<IEnumerable<ProductImage>> GetAllByProductId(string productId);
    }
}
