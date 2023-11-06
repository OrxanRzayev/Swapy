using MediatR;
using Swapy.Common.DTO.Shops.Responses;

namespace Swapy.BLL.Domain.Shops.Queries
{
    public class GetAllShopsQuery : IRequest<ShopsResponseDTO>
    {
        public int Page { get; set; }
        public int PageSize { get; set; }
        public string? Title { get; set; }
        public bool? SortByViews { get; set; }
        public bool? ReverseSort { get; set; }
    }
}
