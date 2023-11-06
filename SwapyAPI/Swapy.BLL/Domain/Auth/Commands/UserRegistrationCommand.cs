using MediatR;
using Swapy.Common.DTO.Auth.Responses;

namespace Swapy.BLL.Domain.Auth.Commands
{
    public class UserRegistrationCommand : IRequest<Unit>
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public string Password { get; set; }
    }
}
