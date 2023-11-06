using FluentValidation;
using Swapy.Common.DTO.Users.Requests;

namespace Swapy.API.Validators
{
    public class UserUpdateValidator : AbstractValidator<UpdateUserCommandDTO>
    {
        public UserUpdateValidator()
        {
            RuleFor(user => user.FirstName)
            .NotEmpty()
                .WithMessage("FirstName field is required")
            .MinimumLength(1)
                .WithMessage("FirstName field should have a minimum length of 1 characters")
            .MaximumLength(64)
                .WithMessage("FirstName field exceeds maximum length of 64 characters")
            .Matches(@"^[A-ZА-ЯƏÜÖĞİŞÇ][A-Za-zА-Яа-яƏəÜüÖöĞğİıŞşÇç\s']{2,31}$")
                .WithMessage("FirstName field has invalid format")
            .WithErrorCode("InvalidFirstNameFormat");

            RuleFor(user => user.LastName)
            .NotEmpty()
                .WithMessage("LastName field is required")
            .MinimumLength(1)
                .WithMessage("LastName field should have a minimum length of 1 characters")
            .MaximumLength(64)
                .WithMessage("LastName field exceeds maximum length of 64 characters")
            .Matches(@"^[A-ZА-ЯƏÜÖĞİŞÇ][A-Za-zА-Яа-яƏəÜüÖöĞğİıŞşÇç\s']{2,31}$")
                .WithMessage("FirstName field has invalid format")
            .WithErrorCode("InvalidLastNameFormat");

            RuleFor(user => user.PhoneNumber)
            .NotEmpty()
                .WithMessage("PhoneNumber field is required")
            .MinimumLength(1)
                .WithMessage("PhoneNumber field should have a minimum length of 1 characters")
            .MaximumLength(64)
                .WithMessage("PhoneNumber field exceeds maximum length of 64 characters")
            .Matches(@"^\+\d{1,3}\d{1,3}\d{7}$")
                .WithMessage("PhoneNumber field has invalid format")
            .WithErrorCode("InvalidPhoneNumberFormat");
        }
    }
}
