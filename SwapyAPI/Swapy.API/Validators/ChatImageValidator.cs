using FluentValidation;

namespace Swapy.API.Validators
{
    public class ChatImageValidator : AbstractValidator<IFormFile>
    {
        public ChatImageValidator()
        {
            RuleFor(x => x)
            .NotEmpty()
                .WithMessage("Image field is required")
            .Must(file => file.Length > 0)
                .WithMessage("Image size cannot be zero")
            .Must(file => file.Length <= 15 * 1024 * 1024)
                .WithMessage("Image size exceeds the maximum allowed limit")
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
