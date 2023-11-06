using MediatR;
using Swapy.Common.DTO.Products.Responses;

namespace Swapy.BLL.Domain.Electronics.Queries
{
    public class GetAllElectronicBrandsQuery : IRequest<IEnumerable<SpecificationResponseDTO<string>>>
    {
        public string ElectronicTypeId { get; set; }
    }
}
