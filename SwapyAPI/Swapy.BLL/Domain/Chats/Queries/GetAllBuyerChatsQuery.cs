using MediatR;
using Swapy.Common.DTO.Chats.Responses;

namespace Swapy.BLL.Domain.Chats.Queries
{
    public class GetAllBuyerChatsQuery : IRequest<ChatsResponseDTO>
    {
        public string UserId { get; set; }
    } 
}