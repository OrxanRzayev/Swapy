using MediatR;
using Swapy.Common.DTO.Auth.Responses;

namespace Swapy.BLL.Domain.Auth.Commands
{
    public class LoginCommand : IRequest<AuthResponseDTO>
    {
        public string EmailOrPhone { get; set; }
        public string Password { get; set; }
    }
}