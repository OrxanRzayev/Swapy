using MediatR;
using Swapy.Common.DTO.Categories.Responses;

namespace Swapy.BLL.Domain.Categories.Queries
{
    public class GetAllSubcategoriesBySubcategoryQuery : IRequest<IEnumerable<CategoryTreeResponseDTO>>
    {
        public string SubcategoryId { get; set; }
    }
}
