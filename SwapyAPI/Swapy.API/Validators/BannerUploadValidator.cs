using FluentValidation;
using Swapy.Common.DTO.Shops.Requests;

namespace Swapy.API.Validators
{
    public class BannerUploadValidator : AbstractValidator<IFormFile>
    {
        public BannerUploadValidator()
        {
            RuleFor(x => x)
            .NotEmpty()
                .WithMessage("Banner field is required")
            .Must(file => file.Length > 0)
                .WithMessage("Banner size cannot be zero")
            .Must(file => file.Length <= 15 * 1024 * 1024)
                .WithMessage("Banner size exceeds the maximum allowed limit")
            .Must(file => IsAllowedExtension(file.FileName))
                .WithMessage("Invalid file extension. Only JPG, JPEG, PNG are allowed");
        }

        private bool IsAllowedExtension(string fileName)
        {
            string[] allowedExtensions = { ".jpg", ".jpeg", ".png" };
            string fileExtension = Path.GetExtension(fileName);
            return allowedExtensions.Contains(fileExtension.ToLower());
        }
    }
}
