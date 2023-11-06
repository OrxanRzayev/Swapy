namespace Swapy.Common.DTO.Shops.Responses
{
    public class ShopDataResponseDTO
    {
        public string ShopName { get; set; }
        public string PhoneNumber { get; set; }
        public string Description { get; set; }
        public string Location { get; set; }
        public string Slogan { get; set; }
        public string Logo { get; set; }
        public string Banner { get; set; }
        public string WorkDays { get; set; }
        public TimeSpan? StartWorkTime { get; set; }
        public TimeSpan? EndWorkTime { get; set; }
    }
}
