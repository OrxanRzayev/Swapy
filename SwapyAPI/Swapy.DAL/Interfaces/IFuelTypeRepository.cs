using Swapy.Common.DTO.Products.Responses;
using Swapy.Common.Entities;

namespace Swapy.DAL.Interfaces
{
    public interface IFuelTypeRepository : IRepository<FuelType>
    {
        Task<IEnumerable<SpecificationResponseDTO<string>>> GetAllSpecificationAsync();
    }
}
