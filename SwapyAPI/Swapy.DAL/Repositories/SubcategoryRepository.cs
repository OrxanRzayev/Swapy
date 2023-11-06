using Microsoft.EntityFrameworkCore;
using Swapy.Common.DTO.Categories.Responses;
using Swapy.Common.DTO.Products.Responses;
using Swapy.Common.Entities;
using Swapy.Common.Enums;
using Swapy.Common.Exceptions;
using Swapy.DAL.Interfaces;

namespace Swapy.DAL.Repositories
{
    public class SubcategoryRepository : ISubcategoryRepository
    {
        private readonly SwapyDbContext _context;

        public SubcategoryRepository(SwapyDbContext context) => _context = context;

        public async Task CreateAsync(Subcategory item)
        {
            await _context.Subcategories.AddAsync(item);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(Subcategory item)
        {
            _context.Subcategories.Update(item);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(Subcategory item)
        {
            _context.Subcategories.Remove(item);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteByIdAsync(string id) => await DeleteAsync(await GetByIdAsync(id));

        public async Task<Subcategory> GetByIdAsync(string id)
        {
            var item = await _context.Subcategories.FindAsync(id);

            if (item == null) throw new NotFoundException($"{GetType().Name.Split("Repository")[0]} with {id} id not found");
            return item;
        }

        public async Task<Subcategory> GetDetailByIdAsync(string id)
        {
            var item = await _context.Subcategories.Include(s => s.ChildSubcategories)
                                                   .Include(s => s.ParentSubcategory)
                                                   .FirstOrDefaultAsync(s => s.Id.Equals(id));

            if (item == null) throw new NotFoundException($"{GetType().Name.Split("Repository")[0]} with {id} id not found");
            return item;
        }

        public async Task<IEnumerable<Subcategory>> GetAllAsync()
        {
            return await _context.Subcategories.ToListAsync();
        }

        /// <summary>
        /// Product Attributes
        /// </summary>
        public async Task<IEnumerable<SpecificationResponseDTO<string>>> GetAllAnimalTypesAsync()
        {
            return (await _context.Subcategories.Where(s => s.Type == CategoryType.AnimalsType)
                                         .Select(s => new SpecificationResponseDTO<string>(s.Id, s.Name))
                                         .ToListAsync())
                                         .OrderBy(s => s.Value);
        }

        public async Task<IEnumerable<SpecificationResponseDTO<string>>> GetAllAutoTypesAsync()
        {
            return (await _context.Subcategories.Where(s => s.Type == CategoryType.AutosType)
                                         .Select(s => new SpecificationResponseDTO<string>(s.Id, s.Name))
                                         .ToListAsync())
                                         .OrderBy(s => s.Value);
        }

        public async Task<IEnumerable<SpecificationResponseDTO<string>>> GetAllElectronicTypesAsync()
        {
            return (await _context.Subcategories.Where(s => s.Type == CategoryType.ElectronicsType)
                                         .Select(s => new SpecificationResponseDTO<string>(s.Id, s.Name))
                                         .ToListAsync())
                                         .OrderBy(s => s.Value);
        }

        public async Task<IEnumerable<SpecificationResponseDTO<string>>> GetAllItemSectionsAsync()
        {
            return (await _context.Subcategories.Include(s => s.ChildSubcategories)
                                         .Where(s => s.Type == CategoryType.ItemsType && s.ChildSubcategories.Count != 0)
                                         .Select(s => new SpecificationResponseDTO<string>(s.Id, s.Name))
                                         .ToListAsync())
                                         .OrderBy(s => s.Value);
        }

        public async Task<IEnumerable<SpecificationResponseDTO<string>>> GetAllItemTypesAsync(string parentSubcategoryId)
        {
            return (await _context.Subcategories.Where(s => s.Type == CategoryType.ItemsType && s.ParentSubcategoryId.Equals(parentSubcategoryId))
                                               .Select(s => new SpecificationResponseDTO<string>(s.Id, s.Name))
                                               .ToListAsync())
                                               .OrderBy(s => s.Value);
        }

        public async Task<IEnumerable<SpecificationResponseDTO<string>>> GetAllRealEstateTypesAsync()
        {
            return (await _context.Subcategories.Where(s => s.Type == CategoryType.RealEstatesType)
                                               .Select(s => new SpecificationResponseDTO<string>(s.Id, s.Name))
                                               .ToListAsync())
                                               .OrderBy(s => s.Value);
        }

        public async Task<IEnumerable<CategoryTreeResponseDTO>> GetByCategoryAsync(string categoryId)
        {
            var item = await _context.Subcategories.Where(s => s.CategoryId.Equals(categoryId) && s.ParentSubcategoryId == null)
                                                   .Include(s => s.Category)
                                                   .Include(s => s.ChildSubcategories)
                                                   .Select(s => new CategoryTreeResponseDTO(s.Id, s.Type, s.SubType, s.Name, s.ChildSubcategories.Count <= 0, s.CategoryId, s.Category.Name))                                                   
                                                   .ToListAsync();

            if (item == null) throw new NotFoundException($"{GetType().Name.Split("Repository")[0]} with {categoryId} id not found");
            return item.OrderBy(s => s.Value);
        }

        public async Task<IEnumerable<CategoryTreeResponseDTO>> GetBySubcategoryAsync(string subcategoryId)
        {
            var item = (await _context.Subcategories.Where(s => s.ParentSubcategoryId.Equals(subcategoryId))
                                                    .Include(s => s.ParentSubcategory)
                                                    .Include(s => s.ChildSubcategories)
                                                    .Select(s => new CategoryTreeResponseDTO(s.Id, s.Type, s.SubType, s.Name, s.ChildSubcategories.Count <= 0, s.ParentSubcategoryId, s.ParentSubcategory.Name))
                                                    .ToListAsync())
                                                    .OrderBy(s => s.Value);

            if (item == null) throw new NotFoundException($"{GetType().Name.Split("Repository")[0]} with {subcategoryId} id not found");
            return item;
        }

        public async Task<IEnumerable<CategoryTreeResponseDTO>> GetSiblings(string subcategoryId)
        {
            var currentSubcategory = await _context.Subcategories.FindAsync(subcategoryId);

            if (currentSubcategory == null) 
            {
                return _context.Categories.AsEnumerable()
                                          .Select(s => new CategoryTreeResponseDTO(s.Id, s.Type, null, s.Name, false, null, null))
                                          .OrderBy(s => s.Value)
                                          .ToList();
            }
            else if(currentSubcategory.ParentSubcategoryId == null)
            {
                return await GetByCategoryAsync(currentSubcategory.CategoryId);
            }
            else
            {
                return await GetBySubcategoryAsync(currentSubcategory.ParentSubcategoryId);
            }
        }

        public async Task<IEnumerable<SpecificationResponseDTO<string>>> GetClothesTypesByGenderAsync(string genderId)
        {
            return (await _context.Subcategories.Include(s => s.ClothesViews)
                                         .Where(s => (s.Type == CategoryType.ClothesType) && (s.ClothesViews.Select(cv => cv.GenderId).Contains(genderId)))
                                         .Select(s => new SpecificationResponseDTO<string>(s.Id, s.Name))
                                         .ToListAsync())
                                         .OrderBy(s => s.Value);
        }

        public async Task<IEnumerable<SpecificationResponseDTO<string>>> GetSequenceOfSubcategories(string subcategoryId)
        {
            List<Subcategory> result = new();
            Subcategory currentSubcategory = await _context.Subcategories.Where(s => s.Id.Equals(subcategoryId))
                                                                         .FirstOrDefaultAsync();

            if (currentSubcategory == null) throw new NotFoundException($"{GetType().Name.Split("Repository")[0]} with {subcategoryId} id not found");

            do
            {
                result.Insert(0, currentSubcategory);
                currentSubcategory = await _context.Subcategories.Where(s => currentSubcategory.ParentSubcategoryId.Equals(s.Id))
                                                                 .FirstOrDefaultAsync();
            } while (currentSubcategory != null);

            return result.Select(s => new SpecificationResponseDTO<string>(s.Id, s.Name)).ToList();
        }

        public async Task<IEnumerable<SpecificationResponseDTO<string>>> GetAllChildsOfSubcategory(string subcategoryId)
        {
            var selectedSubcategory = _context.Subcategories.Include(s => s.ChildSubcategories).FirstOrDefault(s => s.Id.Equals(subcategoryId));
            if (selectedSubcategory != null && selectedSubcategory.ChildSubcategories.Count() <= 0)
            {
                var result = new List<SpecificationResponseDTO<string>>();
                result.Add(new SpecificationResponseDTO<string>(selectedSubcategory.Id, selectedSubcategory.Name));
                return result;
            }
            var subtree = new List<SpecificationResponseDTO<string>>();
            foreach (var childSubcategory in selectedSubcategory.ChildSubcategories)
            {
                subtree.AddRange(await GetAllChildsOfSubcategory(childSubcategory.Id));
            }

            if(subtree.Count > 0)
            {
                return subtree;
            }
            return null;
        }

    }
}
