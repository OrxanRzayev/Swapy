using MediatR;
using Swapy.Common.DTO.Categories.Responses;

namespace Swapy.BLL.Domain.Categories.Queries
{
    public class GetSiblingsQuery : IRequest<IEnumerable<CategoryTreeResponseDTO>>
    {
        public string SubcategoryId { get; set; }
    }
}
