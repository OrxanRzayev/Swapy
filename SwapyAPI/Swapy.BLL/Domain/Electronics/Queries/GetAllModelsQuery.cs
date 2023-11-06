using MediatR;
using Swapy.Common.DTO.Products.Responses;

namespace Swapy.BLL.Domain.Electronics.Queries
{
    public class GetAllModelsQuery : IRequest<IEnumerable<SpecificationResponseDTO<string>>>
    {
        public List<string> ElectronicBrandsId { get; set; }
        public string ElectronicTypeId { get; set; }
    }
}
