using MediatR;

namespace Swapy.BLL.Domain.Products.Commands
{
    public class UploadImageCommand : IRequest<Unit>
    {
        public string ProductId { get; set; }
        public List<string> Paths { get; set; }
    }
}
