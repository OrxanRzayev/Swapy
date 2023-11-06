using FluentValidation;
using Swapy.Common.DTO.Auth.Requests;

namespace Swapy.API.Validators
{
    public class LoginValidator : AbstractValidator<LoginCommandDTO>
    {
        public LoginValidator()
        {
            RuleFor(login => login.EmailOrPhone)
            .NotEmpty()
                .WithMessage("EmailOrPhone field is required")
            .MinimumLength(1)
                .WithMessage("EmailOrPhone field should have a minimum length of 1 characters")
            .MaximumLength(64)
                .WithMessage("EmailOrPhone field exceeds maximum length of 64 characters")
            .WithErrorCode("InvalidEmailOrPhoneFormat");

            RuleFor(user => user.Password)
            .NotEmpty()
                .WithMessage("Password field is required")
            .MinimumLength(8)
                .WithMessage("Password field should have a minimum length of 8 characters")
            .MaximumLength(32)
                .WithMessage("Password field exceeds maximum length of 32 characters")
            .Matches(@"^(?=.*\d)(?=.*[A-Z])(?=.*[a-z])(?=.*[^\w\d\s:])([^\s]){8,32}$")
                .WithMessage("Password field has invalid format")
            .WithErrorCode("InvalidPasswordFormat");
        }
    }
}
