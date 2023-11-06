using FluentValidation;
using Swapy.Common.DTO.Users.Requests;

namespace Swapy.API.Validators
{
    public class RemoveUserValidator : AbstractValidator<RemoveUserCommandDTO>
    {
        public RemoveUserValidator()
        {
            RuleFor(user => user.Token)
            .NotEmpty()
                .WithMessage("Token is required")
            .WithErrorCode("InvalidTokenFormat");
        }
    }
}
