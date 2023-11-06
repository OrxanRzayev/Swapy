using MediatR;
using Microsoft.AspNetCore.Identity;
using Swapy.BLL.Domain.Users.Queries;
using Swapy.Common.DTO.Users.Responses;
using Swapy.Common.Entities;

namespace Swapy.BLL.Domain.Users.QueryHandlers
{
    public class GetUserDataQueryHandler : IRequestHandler<GetUserDataQuery, UserDataResponseDTO>
    {
        private readonly UserManager<User> _userManager;

        public GetUserDataQueryHandler(UserManager<User> userManager) => _userManager = userManager;

        public async Task<UserDataResponseDTO> Handle(GetUserDataQuery request, CancellationToken cancellationToken)
        {
            var user = await _userManager.FindByIdAsync(request.UserId);

            return new UserDataResponseDTO()
            {
                Logo = user.Logo,
                LastName = user.LastName,
                FirstName = user.FirstName,
                PhoneNumber = user.PhoneNumber,
                IsSubscribed = user.IsSubscribed
            };
        }
    }
}
