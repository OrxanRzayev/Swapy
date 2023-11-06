using Microsoft.AspNetCore.Http;

namespace Swapy.Common.DTO.Products.Requests.Commands
{
    public class UploadImagesCommandDTO
    {
        public IFormFileCollection Files { get; set; }
    }
}
