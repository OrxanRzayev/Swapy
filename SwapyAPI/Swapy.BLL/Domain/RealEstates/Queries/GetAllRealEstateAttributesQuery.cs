using MediatR;
using Swapy.Common.DTO.RealEstates.Responses;

namespace Swapy.BLL.Domain.RealEstates.Queries
{
    public class GetAllRealEstateAttributesQuery : IRequest<RealEstateAttributesResponseDTO>
    {
        public string UserId { get; set; }
        public int Page { get; set; }
        public int PageSize { get; set; }
        public string? Title { get; set; }
        public string? CurrencyId { get; set; }
        public decimal? PriceMin { get; set; }
        public decimal? PriceMax { get; set; }
        public string? CategoryId { get; set; }
        public string? SubcategoryId { get; set; }
        public string? CityId { get; set; }
        public string? OtherUserId { get; set; }
        public bool? IsDisable { get; set; }
        public bool? SortByPrice { get; set; }
        public bool? ReverseSort { get; set; }
        public int? AreaMin { get; set; }
        public int? AreaMax { get; set; }
        public int? RoomsMin { get; set; }
        public int? RoomsMax { get; set; }
        public bool? IsRent { get; set; }
        public List<string>? RealEstateTypesId { get; set; }
    }
}
