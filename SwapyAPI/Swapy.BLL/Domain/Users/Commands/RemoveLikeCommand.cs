using MediatR;

namespace Swapy.BLL.Domain.Users.Commands
{
    public class RemoveLikeCommand : IRequest<Unit>
    {
        public string LikerId { get; set; }
        public string RecipientId { get; set; }
    }
}
