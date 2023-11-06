using MediatR;
using Microsoft.AspNetCore.Identity;
using Swapy.BLL.Domain.Users.Commands;
using Swapy.Common.Entities;
using Swapy.Common.Enums;
using Swapy.Common.Exceptions;
using Swapy.DAL.Interfaces;

namespace Swapy.BLL.Domain.Users.CommandHandlers
{
    public class AddLikeCommandHandler : IRequestHandler<AddLikeCommand, Like>
    {
        private readonly UserManager<User> _userManager;
        private readonly ILikeRepository _likeRepository;
        private readonly IUserLikeRepository _userLikeRepository;

        public AddLikeCommandHandler(ILikeRepository likeRepository, IUserLikeRepository userLikeRepository, UserManager<User> userManager)
        {
            _userManager = userManager;
            _likeRepository = likeRepository;
            _userLikeRepository = userLikeRepository;
        }

        public async Task<Like> Handle(AddLikeCommand request, CancellationToken cancellationToken)
        {
            if(await _userLikeRepository.CheckUserLikeAsync(request.UserId, request.RecipientId)) throw new DuplicateValueException("The provided LikerId already liked Recepient");

            if (request.UserId.Equals(request.RecipientId)) throw new DuplicateValueException("The provided LikerId and RecepientId are the same");

            if (request.Type != UserType.Seller) throw new InvalidOperationException("The provided item Id can't like other users");
            
            var like = new Like() { LikerId = request.UserId };
            var userLike = new UserLike(request.RecipientId, like.Id);
            await _userLikeRepository.CreateAsync(userLike);
            
            like.UserLikeId = userLike.Id;
            await _likeRepository.CreateAsync(like);

            var user = await _userManager.FindByIdAsync(request.RecipientId);
            user.LikesCount++;
            await _userManager.UpdateAsync(user);

            return like;
        }
    }
}
