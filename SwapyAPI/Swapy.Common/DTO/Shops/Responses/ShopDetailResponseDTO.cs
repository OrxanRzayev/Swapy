using Swapy.Common.Enums;

namespace Swapy.Common.DTO.Shops.Responses
{
    public class ShopDetailResponseDTO
    {
        public string Id { get; set; }
        public string UserId { get; set; }
        public string ShopName { get; set; }
        public string Description { get; set; }
        public string Email { get; set; }
        public string Location { get; set; }
        public string PhoneNumber { get; set; }
        public string Slogan { get; set; }
        public int Views { get; set; }
        public string Banner { get; set; }
        public string WorkDays { get; set; }
        public string Logo { get; set; }
        public int LikesCount { get; set; }
        public int ProductsCount { get; set; }
        public int SubscriptionsCount { get; set; }
        public TimeSpan? EndWorkTime { get; set; }
        public TimeSpan? StartWorkTime { get; set; }
        public DateTime RegistrationDate { get; set; }
        public UserType Type { get; set;}
    }
}
