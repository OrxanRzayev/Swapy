namespace Swapy.Common.DTO.Shops.Responses
{
    public class ShopsResponseDTO
    {
        public IEnumerable<ShopResponseDTO> Items { get; set; }
        public int Count { get; set; }
        public int AllPages { get; set; }

        public ShopsResponseDTO(IEnumerable<ShopResponseDTO> items, int count, int allPages)
        {
            Items = items;
            Count = count;
            AllPages = allPages;
        }
    }
}
