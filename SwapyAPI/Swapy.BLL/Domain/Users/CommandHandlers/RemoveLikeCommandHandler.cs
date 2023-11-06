using MediatR;
using Microsoft.AspNetCore.Identity;
using Swapy.BLL.Domain.Users.Commands;
using Swapy.Common.Entities;
using Swapy.Common.Exceptions;
using Swapy.DAL.Interfaces;

namespace Swapy.BLL.Domain.Users.CommandHandlers
{
    public class RemoveLikeCommandHandler : IRequestHandler<RemoveLikeCommand, Unit>
    {
        private readonly UserManager<User> _userManager;
        private readonly IUserLikeRepository _userLikeRepository;

        public RemoveLikeCommandHandler(IUserLikeRepository userLikeRepository, UserManager<User> userManager)
        {
            _userManager = userManager;
            _userLikeRepository = userLikeRepository;
        }

        public async Task<Unit> Handle(RemoveLikeCommand request, CancellationToken cancellationToken)
        {
            var userLike = await _userLikeRepository.GetUserLikeByRecipientAsync(request.LikerId, request.RecipientId);

            if (userLike.Like.LikerId != request.LikerId) throw new NoAccessException("No access to delete this like");

            var user = await _userManager.FindByIdAsync(request.RecipientId);
            user.LikesCount--;
            await _userLikeRepository.DeleteAsync(userLike);

            return Unit.Value;
        }
    }
}
