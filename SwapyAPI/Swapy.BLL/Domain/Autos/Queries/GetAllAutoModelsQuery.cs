using MediatR;
using Swapy.Common.DTO.Products.Responses;

namespace Swapy.BLL.Domain.Autos.Queries
{
    public class GetAllAutoModelsQuery : IRequest<IEnumerable<SpecificationResponseDTO<string>>>
    {
        public List<string> AutoBrandsId { get; set; }
        public List<string> AutoTypesId { get; set; }
    }
}
