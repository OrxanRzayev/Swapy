using MediatR;
using Swapy.Common.DTO.Autos.Responses;

namespace Swapy.BLL.Domain.Autos.Queries
{
    public class GetAllAutoAttributesQuery : IRequest<AutoAttributesResponseDTO>
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
        public int? MiliageMin { get; set; }
        public int? MiliageMax { get; set; }
        public int? EngineCapacityMin { get; set; }
        public int? EngineCapacityMax { get; set; }
        public DateTime? ReleaseYearOlder { get; set; }
        public DateTime? ReleaseYearNewer { get; set; }
        public bool? IsNew { get; set; }
        public List<string>? FuelTypesId { get; set; }
        public List<string>? AutoColorsId { get; set; }
        public List<string>? TransmissionTypesId { get; set; }
        public List<string>? AutoBrandsId { get; set; }
        public List<string>? AutoTypesId { get; set; }
    }
}
