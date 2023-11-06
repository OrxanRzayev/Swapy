namespace Swapy.Common.DTO.Products.Requests.Commands
{
    public class UploadImageCommandDTO
    {
        public string ProductId { get; set; }
        public List<string> Paths { get; set; }
    }
}
