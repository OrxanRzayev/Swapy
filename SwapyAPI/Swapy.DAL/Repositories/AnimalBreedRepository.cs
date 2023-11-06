using Microsoft.EntityFrameworkCore;
using Swapy.Common.DTO.Products.Responses;
using Swapy.Common.Entities;
using Swapy.Common.Enums;
using Swapy.Common.Exceptions;
using Swapy.DAL.Interfaces;

namespace Swapy.DAL.Repositories
{
    public class AnimalBreedRepository : IAnimalBreedRepository
    {
        private readonly SwapyDbContext _context;

        public AnimalBreedRepository(SwapyDbContext context) => _context = context;

        public async Task CreateAsync(AnimalBreed item)
        {
            await _context.AnimalBreeds.AddAsync(item);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(AnimalBreed item)
        {
            _context.AnimalBreeds.Update(item);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(AnimalBreed item)
        {
            _context.AnimalBreeds.Remove(item);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteByIdAsync(string id) => await DeleteAsync(await GetByIdAsync(id));

        public async Task<AnimalBreed> GetByIdAsync(string id)
        {
            var item = await _context.AnimalBreeds.FindAsync(id);
            if (item == null) throw new NotFoundException($"{GetType().Name.Split("Repository")[0]} with {id} id not found");
            return item;
        }

        public async Task<IEnumerable<AnimalBreed>> GetAllAsync()
        {
            return await _context.AnimalBreeds.ToListAsync();
        }

        public async Task<IEnumerable<SpecificationResponseDTO<string>>> GetByAnimalTypeAsync(string animalType)
        {
            return _context.AnimalBreeds.Where(x => animalType == null || x.AnimalTypeId.Equals(animalType))
                                        .AsEnumerable()
                                        .Select(s => new SpecificationResponseDTO<string>(s.Id, s.Name))
                                        .OrderBy(s => s.Value)
                                        .ToList();
        }
    }
}
