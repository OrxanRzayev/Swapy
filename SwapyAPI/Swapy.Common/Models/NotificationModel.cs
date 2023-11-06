namespace Swapy.Common.Models
{
    public class NotificationModel
    {
        public string UserId { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string CityId { get; set; }
        public decimal Price { get; set; }
        public string CurrencyId { get; set; }
        public string ProductId { get; set; }
    }
}
