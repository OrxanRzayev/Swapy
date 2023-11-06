using Swapy.BLL.Interfaces;
using Swapy.Common.Entities;
using Swapy.DAL.Interfaces;

namespace Swapy.BLL.Services
{
    public class SubcategoryService : ISubcategoryService
    {
        private ISubcategoryRepository _subcategoryRepository { get; set; }

        public SubcategoryService(ISubcategoryRepository subcategoryRepository) => _subcategoryRepository = subcategoryRepository;
        
        public async Task<bool> SubcategoryValidationAsync(string SubcategoryId)
        {
            Subcategory subcategory = await _subcategoryRepository.GetDetailByIdAsync(SubcategoryId);
            return subcategory.ChildSubcategories.Count == 0;
        }
    }
}
