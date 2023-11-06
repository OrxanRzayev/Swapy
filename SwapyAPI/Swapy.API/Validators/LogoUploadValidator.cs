using FluentValidation;

namespace Swapy.API.Validators
{
    public class LogoUploadValidator : AbstractValidator<IFormFile>
    {
        public LogoUploadValidator()
        {
            RuleFor(x => x)
            .NotEmpty()
                .WithMessage("Logo field is required")
            .Must(file => file.Length > 0)
                .WithMessage("Logo size cannot be zero")
            .Must(file => file.Length <= 10 * 1024 * 1024)
                .WithMessage("Logo size exceeds the maximum allowed limit")
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
