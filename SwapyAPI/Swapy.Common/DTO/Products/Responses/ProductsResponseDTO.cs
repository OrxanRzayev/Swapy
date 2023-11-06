namespace Swapy.Common.DTO.Products.Responses
{
    public class ProductsResponseDTO<T>
    {
        public IEnumerable<T> Items { get; set; }
        public int Count { get; set; }
        public int AllPages { get; set; }
        public decimal? MaxPrice { get; set; }
        public decimal? MinPrice { get; set; }

        public ProductsResponseDTO(IEnumerable<T> items, int count, int allPages, decimal? maxPrice, decimal? minPrice)
        {
            Items = items;
            Count = count;
            AllPages = allPages;
            MaxPrice = maxPrice;
            MinPrice = minPrice;
        }
    }
}
