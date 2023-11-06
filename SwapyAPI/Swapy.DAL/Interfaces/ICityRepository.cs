using Swapy.Common.DTO.Products.Responses;
using Swapy.Common.Entities;

namespace Swapy.DAL.Interfaces
{
    public interface ICityRepository : IRepository<City>
    {
        Task<string> GetLocalizeByIdAsync(string cityId);
        Task<IEnumerable<SpecificationResponseDTO<string>>> GetAllSpecificationAsync();
    }
}
