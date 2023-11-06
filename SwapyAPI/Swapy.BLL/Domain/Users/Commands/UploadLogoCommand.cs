using MediatR;
using Microsoft.AspNetCore.Http;

namespace Swapy.BLL.Domain.Users.Commands
{
    public class UploadLogoCommand : IRequest<Unit>
    {
        public string UserId { get; set; }
        public IFormFile Logo;
    }
}
