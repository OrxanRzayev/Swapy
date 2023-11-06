using FluentValidation;
using Swapy.Common.DTO.Auth.Requests;

namespace Swapy.API.Validators
{
    public class ConfirmEmailValidator : AbstractValidator<ConfirmEmailDTO>
    {
        public ConfirmEmailValidator()
        {
            RuleFor(confirm => confirm.UserId)
            .NotEmpty()
                .WithMessage("UserId is required")
            .Matches(@"^[0-9A-Fa-f]{8}[-]?([0-9A-Fa-f]{4}[-]?){3}[0-9A-Fa-f]{12}$")
                .WithMessage("UserId has invalid format")
            .WithErrorCode("InvalidUserIdFormat");

            RuleFor(confirm => confirm.Token)
            .NotEmpty()
                .WithMessage("Token is required")
            .WithErrorCode("InvalidTokenFormat");
        }
    }
}
