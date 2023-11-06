namespace Swapy.Common.DTO.Shops.Requests
{
    public class GetAllShopsQueryDTO
    {
        public string? Title { get; set; }
        public int Page { get; set; }
        public int PageSize { get; set; }
        public bool? SortByViews { get; set; }
        public bool? ReverseSort { get; set; }
    }
}
