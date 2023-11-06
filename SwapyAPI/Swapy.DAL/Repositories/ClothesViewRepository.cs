using Microsoft.EntityFrameworkCore;
using Swapy.Common.DTO.Products.Responses;
using Swapy.Common.Entities;
using Swapy.Common.Exceptions;
using Swapy.DAL.Interfaces;

namespace Swapy.DAL.Repositories
{
    public class ClothesViewRepository : IClothesViewRepository
    {
        private readonly SwapyDbContext _context;

        public ClothesViewRepository(SwapyDbContext context) => _context = context;

        public async Task CreateAsync(ClothesView item)
        {
            await _context.ClothesViews.AddAsync(item);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(ClothesView item)
        {
            _context.ClothesViews.Update(item);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(ClothesView item)
        {
            _context.ClothesViews.Remove(item);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteByIdAsync(string id) => await DeleteAsync(await GetByIdAsync(id));

        public async Task<ClothesView> GetByIdAsync(string id)
        {
            var item = await _context.ClothesViews.FindAsync(id);
            if (item == null) throw new NotFoundException($"{GetType().Name.Split("Repository")[0]} with {id} id not found");
            return item;
        }

        public async Task<IEnumerable<ClothesView>> GetAllAsync()
        {
            return await _context.ClothesViews.ToListAsync();
        }

        public async Task<IEnumerable<SpecificationResponseDTO<string>>> GetByGenderAndTypeAsync(bool? isChild, string genderId, string clothesTypeId)
        {
            return _context.ClothesViews.Where(x => (isChild == null || x.IsChild == isChild) && (clothesTypeId == null || x.ClothesTypeId.Equals(clothesTypeId)) && (genderId == null || x.GenderId.Equals(genderId)))
                                        .AsEnumerable()
                                        .Select(s => new SpecificationResponseDTO<string>(s.Id, s.Name))
                                        .OrderBy(s => s.Value)
                                        .ToList();
        }
    }
}
