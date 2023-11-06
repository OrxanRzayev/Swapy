using Swapy.Common.DTO.Products.Responses;
using Swapy.Common.Entities;

namespace Swapy.DAL.Interfaces
{
    public interface IColorRepository : IRepository<Color>
    {
        Task<IEnumerable<SpecificationResponseDTO<string>>> GetByModelAsync(string modelId);
        Task<IEnumerable<SpecificationResponseDTO<string>>> GetAllSpecificationAsync();
    }
}
