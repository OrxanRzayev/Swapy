using FluentValidation;
using Swapy.Common.DTO.Auth.Requests;

namespace Swapy.API.Validators
{
    public class ResetPasswordValidator : AbstractValidator<ResetPasswordCommandDTO>
    {
        public ResetPasswordValidator()
        {
            RuleFor(auth => auth.UserId)
            .NotEmpty()
                .WithMessage("UserId field is required")
            .Matches(@"^[0-9A-Fa-f]{8}[-]?([0-9A-Fa-f]{4}[-]?){3}[0-9A-Fa-f]{12}$")
                .WithMessage("UserId field has invalid format")
            .WithErrorCode("InvalidUserIdFormat");

            RuleFor(auth => auth.Password)
            .NotEmpty()
                .WithMessage("Password field is required")
            .MinimumLength(8)
                .WithMessage("Password field should have a minimum length of 8 characters")
            .MaximumLength(32)
                .WithMessage("Password field exceeds maximum length of 32 characters")
            .Matches(@"^(?=.*\d)(?=.*[A-Z])(?=.*[a-z])(?=.*[^\w\d\s:])([^\s]){8,32}$")
                .WithMessage("Password field has invalid format")
            .WithErrorCode("InvalidPasswordFormat");

            RuleFor(auth => auth.Token)
            .NotEmpty()
                .WithMessage("Token is required")
            .WithErrorCode("InvalidTokenFormat");
        }
    }
}
