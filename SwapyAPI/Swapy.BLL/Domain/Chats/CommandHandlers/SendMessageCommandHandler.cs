using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.SignalR;
using Microsoft.EntityFrameworkCore;
using Swapy.BLL.Domain.Chats.Commands;
using Swapy.BLL.Hubs;
using Swapy.BLL.Interfaces;
using Swapy.Common.Entities;
using Swapy.Common.Enums;
using Swapy.Common.Models;
using Swapy.DAL.Interfaces;

namespace Swapy.BLL.Domain.Chats.CommandHandlers
{
    public class SendMessageCommandHandler : IRequestHandler<SendMessageCommand, Message>
    {
        private readonly IImageService _imageService;
        private readonly UserManager<User> _userManager;
        private readonly IChatRepository _chatRepository;
        private readonly IHubContext<ChatHub> _chatHubContext;
        private readonly IMessageRepository _messageRepository;

        public SendMessageCommandHandler(IImageService imageService, UserManager<User> userManager, IChatRepository chatRepository, IHubContext<ChatHub> chatHubContext, IMessageRepository messageRepository)
        {
            _imageService = imageService;
            _userManager = userManager;
            _chatRepository = chatRepository;
            _chatHubContext = chatHubContext;
            _messageRepository = messageRepository;
        }

        public async Task<Message> Handle(SendMessageCommand request, CancellationToken cancellationToken)
        {
            string? imagePath = null;

            if (request.Image != null) imagePath = await _imageService.UploadChatImagesAsync(request.Image);

            var message = new Message(request.Text, imagePath, request.ChatId, request.UserId);

            await _messageRepository.CreateAsync(message);

            await _chatRepository.UpdateChatState(request.ChatId, false);

            //Chat Hub Logic
            var recepient = await _chatRepository.GetChatRecepientIdAsync(request.ChatId, request.UserId);

            var tmp = ChatHub.GetConnectedClients();
            var chatRecepient = ChatHub.GetConnectedClients().FirstOrDefault(c => c.UserId.Equals(recepient.Id));
            var chatSender = ChatHub.GetConnectedClients().FirstOrDefault(c => c.UserId.Equals(message.SenderId));

            var model = new ChatMessageModel(message.ChatId, recepient.Id, message.SenderId, string.Empty, message.Text, message.Image, message.DateTime);
            
            await _chatHubContext.Clients.Client(chatSender.ConnectionId).SendAsync("ReceiveMessage", model);

            if (chatRecepient != null) 
            {
                var sender = await _userManager.Users.Where(u => u.Id == request.UserId)
                                                     .Include(u => u.ShopAttribute)
                                                     .FirstOrDefaultAsync();

                var senderName = sender.Type == UserType.Seller ? $"{sender.FirstName} {sender.LastName}" : sender.ShopAttribute.ShopName;
                model.SenderName = senderName;

                await _chatHubContext.Clients.Client(chatRecepient.ConnectionId).SendAsync("ReceiveMessage", model);
            }
            else
            {
                if (!recepient.HasUnreadMessages)
                {
                    recepient.HasUnreadMessages = true;
                    await _userManager.UpdateAsync(recepient);
                }
            }

            return message;
        }
    }
}