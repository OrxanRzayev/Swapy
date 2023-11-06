using Swapy.Common.DTO.Categories.Responses;
using Swapy.Common.DTO.Products.Responses;
using Swapy.Common.Entities;

namespace Swapy.DAL.Interfaces
{
    public interface ISubcategoryRepository : IRepository<Subcategory>
    {
        Task<Subcategory> GetDetailByIdAsync(string id);
        Task<IEnumerable<CategoryTreeResponseDTO>> GetByCategoryAsync(string categoryId);
        Task<IEnumerable<CategoryTreeResponseDTO>> GetBySubcategoryAsync(string subcategoryId);
        Task<IEnumerable<SpecificationResponseDTO<string>>> GetAllAutoTypesAsync();
        Task<IEnumerable<SpecificationResponseDTO<string>>> GetClothesTypesByGenderAsync(string genderId);
        Task<IEnumerable<SpecificationResponseDTO<string>>> GetAllElectronicTypesAsync();
        Task<IEnumerable<SpecificationResponseDTO<string>>> GetAllRealEstateTypesAsync();
        Task<IEnumerable<SpecificationResponseDTO<string>>> GetAllAnimalTypesAsync();
        Task<IEnumerable<CategoryTreeResponseDTO>> GetSiblings(string subcategoryId);
        Task<IEnumerable<SpecificationResponseDTO<string>>> GetAllItemSectionsAsync();
        Task<IEnumerable<SpecificationResponseDTO<string>>> GetAllItemTypesAsync(string parentSubcategoryId);
        Task<IEnumerable<SpecificationResponseDTO<string>>> GetSequenceOfSubcategories(string subcategoryId);
        Task<IEnumerable<SpecificationResponseDTO<string>>> GetAllChildsOfSubcategory(string subcategoryId);
    }
}
