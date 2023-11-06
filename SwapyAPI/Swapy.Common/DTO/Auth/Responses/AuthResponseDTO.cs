using Swapy.Common.Enums;

namespace Swapy.Common.DTO.Auth.Responses
{
    public class AuthResponseDTO
    {
        public UserType Type { get; set; }
        public string UserId { get; set; }
        public string AccessToken { get; set; }
        public string RefreshToken { get; set; }
        public bool HasUnreadMessages { get; set; }
    }
}
