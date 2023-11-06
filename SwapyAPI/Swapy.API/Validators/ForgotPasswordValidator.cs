using FluentValidation;
using Swapy.Common.DTO.Auth.Requests;

namespace Swapy.API.Validators
{
    public class ForgotPasswordValidator : AbstractValidator<ForgotPasswordCommandDTO>
    {
        public ForgotPasswordValidator()
        {
            RuleFor(auth => auth.Email)
            .NotEmpty()
                .WithMessage("Email field is required")
            .MinimumLength(1)
                .WithMessage("Email field should have a minimum length of 1 characters")
            .MaximumLength(64)
                .WithMessage("Email field exceeds maximum length of 64 characters")
            .Matches(@"^([0-9A-Za-z]{1}[-0-9A-z\.]{1,}[0-9A-Za-z]{1}@([-A-Za-z]{1,}\.){1,2}[-A-Za-z]{2,})$")
                .WithMessage("Email field has invalid format")
            .WithErrorCode("InvalidEmailFormat");
        }
    }
}