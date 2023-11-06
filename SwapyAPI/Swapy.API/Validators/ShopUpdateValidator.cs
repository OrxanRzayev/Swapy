using FluentValidation;
using Swapy.Common.DTO.Shops.Requests;

namespace Swapy.API.Validators
{
    public class ShopUpdateValidator : AbstractValidator<UpdateShopCommandDTO>
    {
        public ShopUpdateValidator()
        {
            RuleFor(shop => shop.ShopName)
            .NotEmpty()
                .WithMessage("ShopName field is required")
            .MinimumLength(1)
                .WithMessage("ShopName field should have a minimum length of 1 characters")
            .MaximumLength(64)
                .WithMessage("ShopName field exceeds maximum length of 64 characters")
            .Matches(@"^([A-ZА-ЯƏÜÖĞİŞÇ]|[0-9])[A-Za-zА-Яа-яƏəÜüÖöĞğİıŞşÇç0-9\s']{2,31}$")
                .WithMessage("ShopName field has invalid format")
            .WithErrorCode("InvalidShopNameFormat");

            RuleFor(shop => shop.Description)
            .MaximumLength(512)
                .WithMessage("Description field exceeds maximum length of 512 characters")
            .WithErrorCode("InvalidDescriptionFormat");

            RuleFor(shop => shop.Location)
            .MaximumLength(256)
                .WithMessage("Location field exceeds maximum length of 256 characters")
            .WithErrorCode("InvalidLocationFormat");

            RuleFor(shop => shop.Slogan)
            .MaximumLength(64)
                .WithMessage("Location field exceeds maximum length of 64 characters")
            .WithErrorCode("InvalidSloganFormat");
        }   
    }
}
