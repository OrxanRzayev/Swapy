using Azure.Storage.Blobs;
using MediatR;
using Microsoft.Extensions.Configuration;
using Swapy.BLL.Domain.Products.Commands;
using Swapy.BLL.Interfaces;
using Swapy.Common.DTO.Products.Responses;

namespace Swapy.BLL.Domain.Products.CommandHandlers
{
    public class PreviewUploadImageCommandHandler : IRequestHandler<PreviewUploadImageCommand, ImageResponseDTO>
    {
        private readonly IKeyVaultService _keyVaultService;

        public PreviewUploadImageCommandHandler(IKeyVaultService keyVaultService) => _keyVaultService = keyVaultService;

        public async Task<ImageResponseDTO> Handle(PreviewUploadImageCommand request, CancellationToken cancellationToken)
        {
            var imagePaths = new List<string>();
            var blob = await _keyVaultService.GetSecretValue("Blob-Storage");
            
            foreach (var image in request.Images)
            {
                var imageName = Guid.NewGuid().ToString() + Path.GetExtension(image.FileName);
                var blobServiceClient = new BlobServiceClient(blob);
                var containerClient = blobServiceClient.GetBlobContainerClient("product-images");

                await containerClient.UploadBlobAsync(imageName, image.OpenReadStream());
                imagePaths.Add(imageName);
            }

            return new ImageResponseDTO() { Paths = imagePaths };
        }
    }
}
