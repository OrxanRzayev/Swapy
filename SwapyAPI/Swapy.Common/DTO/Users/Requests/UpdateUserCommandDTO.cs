namespace Swapy.Common.DTO.Users.Requests
{
    public class UpdateUserCommandDTO
    {
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public string? PhoneNumber { get; set; }
        public bool IsSubscribed { get; set; }
    }
}
