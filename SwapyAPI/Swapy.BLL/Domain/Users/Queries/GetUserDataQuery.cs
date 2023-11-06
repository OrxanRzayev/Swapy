using MediatR;
using Swapy.Common.DTO.Users.Responses;

namespace Swapy.BLL.Domain.Users.Queries
{
    public class GetUserDataQuery : IRequest<UserDataResponseDTO>
    {
        public string UserId { get; set; }
    }
}
