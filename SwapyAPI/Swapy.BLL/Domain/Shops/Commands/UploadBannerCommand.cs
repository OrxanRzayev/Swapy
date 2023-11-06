using MediatR;
using Microsoft.AspNetCore.Http;

namespace Swapy.BLL.Domain.Shops.Commands
{
    public class UploadBannerCommand : IRequest<Unit>
    {
        public string UserId { get; set; }
        public IFormFile Banner { get; set; }
    }
}
