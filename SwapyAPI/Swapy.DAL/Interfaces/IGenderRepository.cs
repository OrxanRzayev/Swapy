using Swapy.Common.DTO.Products.Responses;
using Swapy.Common.Entities;

namespace Swapy.DAL.Interfaces
{
    public interface IGenderRepository : IRepository<Gender>
    {
        Task<IEnumerable<SpecificationResponseDTO<string>>> GetAllSpecificationAsync();
    }
}
