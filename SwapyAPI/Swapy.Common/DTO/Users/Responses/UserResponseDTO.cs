using Swapy.Common.Enums;

namespace Swapy.Common.DTO.Users.Responses
{
    public class UserResponseDTO
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public DateTime RegistrationDate { get; set; }
        public string Logo { get; set; }
        public int LikesCount { get; set; }
        public int ProductsCount { get; set; }
        public int SubscriptionsCount { get; set; }
        public UserType Type { get; set; }
    }
}
