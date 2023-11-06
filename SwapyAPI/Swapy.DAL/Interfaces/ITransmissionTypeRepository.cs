using Swapy.Common.DTO.Products.Responses;
using Swapy.Common.Entities;

namespace Swapy.DAL.Interfaces
{
    public interface ITransmissionTypeRepository : IRepository<TransmissionType>
    {
        Task<IEnumerable<SpecificationResponseDTO<string>>> GetAllSpecificationAsync();
    }
}
