using MediatR;
using Swapy.BLL.Domain.Chats.Queries;
using Swapy.Common.DTO.Chats.Responses;
using Swapy.Common.Enums;
using Swapy.DAL.Interfaces;

namespace Swapy.BLL.Domain.Chats.QueryHandlers
{
    public class GetDetailChatQueryHandler : IRequestHandler<GetDetailChatQuery, DetailChatResponseDTO>
    {
        private readonly IChatRepository _chatRepository;

        public GetDetailChatQueryHandler(IChatRepository chatRepository) => _chatRepository = chatRepository;

        public async Task<DetailChatResponseDTO> Handle(GetDetailChatQuery request, CancellationToken cancellationToken)
        {
            var chat = await _chatRepository.GetByIdDetailAsync(request.ChatId);
            
            if(await _chatRepository.TryReadMessage(request.UserId, request.ChatId)) await _chatRepository.UpdateChatState(chat.Id, true);

            return new DetailChatResponseDTO()
            {
                ChatId = chat.Id,
                Messages = chat.Messages.Select(x => new MessageResponseDTO()
                {
                    Id = x.Id,
                    Text = x.Text,
                    Image = x.Image,
                    ChatId = x.ChatId,
                    DateTime = x.DateTime,
                    SenderId = x.SenderId,
                    SenderLogo = x.Sender.Logo
                }),

                Title = request.UserId.Equals(chat.BuyerId) ? chat.Product.Title : chat.Buyer.Type == UserType.Seller ? $"{chat.Buyer.FirstName} {chat.Buyer.LastName}" : chat.Buyer.ShopAttribute.ShopName,
                Image = request.UserId.Equals(chat.BuyerId) ? (chat.Product.Images.FirstOrDefault()?.Image == null ? "default-product-image.png" : chat.Product.Images.FirstOrDefault()?.Image) : chat.Buyer.Logo,
                Type = request.UserId.Equals(chat.BuyerId) ? ChatType.Buyyer : ChatType.Seller
            };
        }
    }
}