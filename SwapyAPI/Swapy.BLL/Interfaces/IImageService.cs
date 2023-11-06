using Microsoft.AspNetCore.Http;

namespace Swapy.BLL.Interfaces
{
    public interface IImageService
    {
        Task<string> UploadChatImagesAsync(IFormFile file);
        Task UploadLogoAsync(IFormFile file, string userId);
        Task UploadBannerAsync(IFormFile file, string userId);
        Task UploadProductImagesAsync(IFormFileCollection files, string productId);
        Task RemoveProductImagesAsync(List<string> paths, string productId);
        Task RemoveAllProductImagesAsync(string productId);
        Task RemoveImageFromBlob(string path, string key);
    }
}
