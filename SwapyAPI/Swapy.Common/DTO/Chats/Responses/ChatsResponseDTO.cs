namespace Swapy.Common.DTO.Chats.Responses
{
    public class ChatsResponseDTO
    {
        public IEnumerable<ChatResponseDTO> Items { get; set; }
        public int Count { get; set; }

        public ChatsResponseDTO(IEnumerable<ChatResponseDTO> items, int count)
        {
            Items = items;
            Count = count;
        }
    }
}
