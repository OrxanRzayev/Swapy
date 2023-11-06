using Swapy.Common.DTO.Products.Responses;
using Swapy.Common.Entities;

namespace Swapy.DAL.Interfaces
{
    public interface IClothesSeasonRepository : IRepository<ClothesSeason>
    {
        Task<IEnumerable<SpecificationResponseDTO<string>>> GetAllSpecificationAsync();
    }
}
