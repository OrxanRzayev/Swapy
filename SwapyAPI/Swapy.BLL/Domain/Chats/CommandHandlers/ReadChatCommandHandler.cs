using MediatR;
using Microsoft.AspNetCore.Identity;
using Swapy.BLL.Domain.Chats.Commands;
using Swapy.Common.Entities;
using Swapy.DAL.Interfaces;

namespace Swapy.BLL.Domain.Chats.CommandHandlers
{
    public class ReadChatCommandHandler : IRequestHandler<ReadChatCommand, bool>
    {
        private readonly UserManager<User> _userManager;
        private readonly IChatRepository _chatRepository;

        public ReadChatCommandHandler(UserManager<User> userManager, IChatRepository chatRepository)
        {
            _userManager = userManager;
            _chatRepository = chatRepository;
        }

        public async Task<bool> Handle(ReadChatCommand request, CancellationToken cancellationToken)
        {
            if (await _chatRepository.TryReadMessage(request.UserId, request.ChatId))
            {
                var chat = await _chatRepository.GetByIdAsync(request.ChatId);
                chat.IsReaded = true;
                await _chatRepository.UpdateAsync(chat);

                foreach(var item in await _chatRepository.GetAllChatsAsync(request.UserId))
                {
                    if (!item.IsReaded) return true;
                }

                var user = await _userManager.FindByIdAsync(request.UserId);
                user.HasUnreadMessages = false;
                await _userManager.UpdateAsync(user);
            }

            return false;
        }
    }
}
