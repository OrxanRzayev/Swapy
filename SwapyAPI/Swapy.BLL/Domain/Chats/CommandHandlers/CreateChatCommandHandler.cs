using MediatR;
using Swapy.BLL.Domain.Chats.Commands;
using Swapy.Common.Entities;
using Swapy.DAL.Interfaces;

namespace Swapy.BLL.Domain.Chats.CommandHandlers
{
    public class CreateChatCommandHandler : IRequestHandler<CreateChatCommand, Chat>
    {
        private readonly IChatRepository _chatRepository;
        private readonly IProductRepository _productRepository;

        public CreateChatCommandHandler(IChatRepository chatRepository, IProductRepository productRepository)
        {
            _chatRepository = chatRepository;
            _productRepository = productRepository;
        }
        public async Task<Chat> Handle(CreateChatCommand request, CancellationToken cancellationToken)
        {
            var chat = await _chatRepository.CheckChatExists(request.UserId, request.ProductId);

            if (chat != null) return chat;

            await _productRepository.GetByIdAsync(request.ProductId);
            var newChat = new Chat(request.ProductId, request.UserId);
            await _chatRepository.CreateAsync(newChat);

            return newChat;
        }
    }
}