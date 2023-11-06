using MediatR;
using Microsoft.AspNetCore.Identity;
using Swapy.BLL.Domain.Users.Queries;
using Swapy.Common.DTO.Users.Responses;
using Swapy.Common.Entities;
using Swapy.Common.Exceptions;

namespace Swapy.BLL.Domain.Users.QueryHandlers
{
    public class GetByIdUserQueryHandler : IRequestHandler<GetByIdUserQuery, UserResponseDTO>
    {
        private readonly UserManager<User> _userManager;

        public GetByIdUserQueryHandler(UserManager<User> userManager) => _userManager = userManager;

        public async Task<UserResponseDTO> Handle(GetByIdUserQuery request, CancellationToken cancellationToken)
        {
            var user = await _userManager.FindByIdAsync(request.UserId);

            if (user == null) throw new NotFoundException($"UserId {request.UserId} not found");
            if (user.EmailConfirmed == false) throw new NotFoundException($"UserId {request.UserId} not found");

            return new UserResponseDTO()
            {
                Logo = user.Logo,
                Email = user.Email,
                LastName = user.LastName,
                FirstName = user.FirstName,
                LikesCount = user.LikesCount,
                PhoneNumber = user.PhoneNumber,
                ProductsCount = user.ProductsCount,
                RegistrationDate = user.RegistrationDate,
                SubscriptionsCount = user.SubscriptionsCount,
                Type = user.Type
            };
        }
    }
}
