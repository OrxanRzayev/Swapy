using FluentValidation;
using Swapy.Common.DTO.Auth.Requests;

namespace Swapy.API.Validators
{
    public class UserRegisterValidator : AbstractValidator<UserRegistrationCommandDTO>
    {
        public UserRegisterValidator()
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

            RuleFor(user => user.Email)
            .NotEmpty()
                .WithMessage("Email field is required")
            .MinimumLength(1)
                .WithMessage("Email field should have a minimum length of 1 characters")
            .MaximumLength(64)
                .WithMessage("Email field exceeds maximum length of 64 characters")
            .Matches(@"^([0-9A-Za-z]{1}[-0-9A-z\.]{1,}[0-9A-Za-z]{1}@([-A-Za-z]{1,}\.){1,2}[-A-Za-z]{2,})$")
                .WithMessage("Email field has invalid format")
            .WithErrorCode("InvalidEmailFormat");

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
