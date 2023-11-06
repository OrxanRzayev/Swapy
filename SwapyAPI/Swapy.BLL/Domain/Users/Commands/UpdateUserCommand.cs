using MediatR;

namespace Swapy.BLL.Domain.Users.Commands
{
    public class UpdateUserCommand : IRequest<Unit>
    {
        public string UserId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string PhoneNumber { get; set; }
        public bool IsSubscribed { get; set; }
    }
}
