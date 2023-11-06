namespace Swapy.Common.DTO.Products.Requests.Queries
{
    public abstract class GetAllBasicProductsQueryDTO<T>
    {
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
    }
}
