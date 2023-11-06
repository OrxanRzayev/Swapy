using FluentValidation;
using Swapy.Common.DTO.Auth.Requests;

namespace Swapy.API.Validators
{
    public class ChangePasswordValidator : AbstractValidator<ChangePasswordCommandDTO>
    {
        public ChangePasswordValidator()
        {
            RuleFor(auth => auth.OldPassword)
            .NotEmpty()
                .WithMessage("Old Password field is required")
            .MinimumLength(8)
                .WithMessage("Old Password field should have a minimum length of 8 characters")
            .MaximumLength(32)
                .WithMessage("Old Password field exceeds maximum length of 32 characters")
            .Matches(@"^(?=.*\d)(?=.*[A-Z])(?=.*[a-z])(?=.*[^\w\d\s:])([^\s]){8,32}$")
                .WithMessage("Old Password field has invalid format")
            .WithErrorCode("InvalidOldPasswordFormat");

            RuleFor(auth => auth.NewPassword)
            .NotEmpty()
                .WithMessage("New Password field is required")
            .MinimumLength(8)
                .WithMessage("New Password field should have a minimum length of 8 characters")
            .MaximumLength(32)
                .WithMessage("New Password field exceeds maximum length of 32 characters")
            .Matches(@"^(?=.*\d)(?=.*[A-Z])(?=.*[a-z])(?=.*[^\w\d\s:])([^\s]){8,32}$")
                .WithMessage("New Password field has invalid format")
            .WithErrorCode("InvalidNewPasswordFormat");
        }
    }
}
