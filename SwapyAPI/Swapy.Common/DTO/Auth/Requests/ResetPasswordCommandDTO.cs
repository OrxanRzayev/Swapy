namespace Swapy.Common.DTO.Auth.Requests
{
    public class ResetPasswordCommandDTO
    {
        public string UserId { get; set; }
        public string Password { get; set; }
        public string Token { get; set; }
    }
}
