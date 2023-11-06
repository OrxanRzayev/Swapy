using Swapy.Common.DTO.Categories.Responses;
using Swapy.Common.DTO.Products.Responses;
using Swapy.Common.Entities;

namespace Swapy.DAL.Interfaces
{
    public interface ICategoryRepository : IRepository<Category>
    {
        Task<IEnumerable<CategoryTreeResponseDTO>> GetAllCategoryTreesAsync();
        Task<SpecificationResponseDTO<string>> GetBySubcategoryIdAsync(string subcategoryId);
    }
}
