using MediatR;
using Swapy.Common.DTO.Categories.Responses;

namespace Swapy.BLL.Domain.Categories.Queries
{
    public class GetAllSubcategoriesByCategoryQuery : IRequest<IEnumerable<CategoryTreeResponseDTO>>
    {
        public string CategoryId { get; set; }
    }
}
