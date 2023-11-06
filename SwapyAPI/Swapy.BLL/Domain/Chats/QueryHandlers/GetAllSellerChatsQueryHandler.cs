using MediatR;
using Swapy.BLL.Domain.Chats.Queries;
using Swapy.Common.DTO.Chats.Responses;
using Swapy.Common.Enums;
using Swapy.DAL.Interfaces;

namespace Swapy.BLL.Domain.Chats.QueryHandlers
{
    public class GetAllSellerChatsQueryHandler : IRequestHandler<GetAllSellerChatsQuery, ChatsResponseDTO>
    {
        private readonly IChatRepository _chatRepository;
         
        public GetAllSellerChatsQueryHandler(IChatRepository chatRepository) => _chatRepository = chatRepository;

        public async Task<ChatsResponseDTO> Handle(GetAllSellerChatsQuery request, CancellationToken cancellationToken)
        {
            var chats = new List<ChatResponseDTO>();

            foreach (var chat in await _chatRepository.GetAllSellerChatsAsync(request.UserId))
            {
                var tmpChat = new ChatResponseDTO();
                tmpChat.ChatId = chat.Id;
                tmpChat.Logo = chat.Buyer.Logo;
                tmpChat.Title = chat.Buyer.Type == UserType.Seller ? $"{chat.Buyer.FirstName} {chat.Buyer.LastName}" : chat.Buyer.ShopAttribute.ShopName;
                tmpChat.Image = chat.Product.Images.FirstOrDefault()?.Image == null ? "default-product-image.png" : chat.Product.Images.FirstOrDefault()?.Image;

                if (chat.Messages == null || chat.Messages.Count == 0)
                {
                    tmpChat.IsReaded = true;
                    tmpChat.LastMessage = string.Empty;
                    tmpChat.LastMessageDateTime = null;
                }
                else
                {
                    tmpChat.IsReaded = chat.Messages.FirstOrDefault().SenderId.Equals(request.UserId) ? true : chat.IsReaded;
                    tmpChat.LastMessage = chat.Messages.FirstOrDefault()?.Text == null ? "📎 Photo" : chat.Messages.FirstOrDefault()?.Text;
                    tmpChat.LastMessageDateTime = chat.Messages.FirstOrDefault()?.DateTime;
                }

                chats.Add(tmpChat);
            }

            return new ChatsResponseDTO(chats, chats.Count());
        }
    }
}