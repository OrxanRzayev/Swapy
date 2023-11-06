using MediatR;
using Microsoft.AspNetCore.Http;
using Swapy.Common.DTO.Products.Responses;

namespace Swapy.BLL.Domain.Products.Commands
{
    public class PreviewUploadImageCommand : IRequest<ImageResponseDTO>
    {
        public string UserId { get; set; }
        public IFormFileCollection Images { get; set; }
    }
}
