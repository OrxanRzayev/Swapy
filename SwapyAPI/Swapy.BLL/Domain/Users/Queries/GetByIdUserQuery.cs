using MediatR;
using Swapy.Common.DTO.Users.Responses;

namespace Swapy.BLL.Domain.Users.Queries
{
    public class GetByIdUserQuery : IRequest<UserResponseDTO>
    {
        public string UserId { get; set; }
    }
}
