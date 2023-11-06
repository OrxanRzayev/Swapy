using MediatR;
using Swapy.Common.DTO.Products.Responses;

namespace Swapy.BLL.Domain.Autos.Queries
{
    public class GetAllAutoBrandsQuery : IRequest<IEnumerable<SpecificationResponseDTO<string>>>
    {
        public List<string> AutoTypesId { get; set; }
    }
}
