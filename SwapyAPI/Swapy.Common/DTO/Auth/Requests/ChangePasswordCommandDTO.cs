namespace Swapy.Common.DTO.Auth.Requests
{
    public class ChangePasswordCommandDTO
    {
        public string OldPassword { get; set; }
        public string NewPassword { get; set; }
    }
}
