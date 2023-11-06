using FluentValidation;
using Swapy.Common.DTO.Products.Requests.Commands;

namespace Swapy.API.Validators
{
    public class AddImageUploadValidator : AbstractValidator<IFormFileCollection>
    {
        public AddImageUploadValidator()
        {
            RuleForEach(x => x)
            .NotEmpty()
                .WithMessage("Logo field is required")
            .Must(file => file.Length > 0)
                .WithMessage("Logo size cannot be zero")
            .Must(file => file.Length <= 20 * 1024 * 1024)
                .WithMessage("Logo size exceeds the maximum allowed limit")
            .Must(file => IsAllowedExtension(file.FileName))
                .WithMessage("Invalid file extension. Only JPG, JPEG, PNG, JFIF are allowed");
        }

        private bool IsAllowedExtension(string fileName)
        {
            string[] allowedExtensions = { ".jpg", ".jpeg", ".png", ".jfif", ".pjpeg", ".pjp" };
            string fileExtension = Path.GetExtension(fileName);
            return allowedExtensions.Contains(fileExtension.ToLower());
        }
    }
}
