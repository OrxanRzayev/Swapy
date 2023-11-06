using FluentValidation;
using Swapy.Common.DTO.Products.Requests.Commands;

namespace Swapy.API.Validators
{
    public class AddProductValidator : AbstractValidator<AddProductCommandDTO>
    {
        public AddProductValidator()
        {
            RuleFor(product => product.Title)
            .NotEmpty()
                .WithMessage("Title field is required")
            .MinimumLength(3)
                .WithMessage("Title field should have a minimum length of 3 characters")
            .MaximumLength(128)
                .WithMessage("Title field exceeds maximum length of 128 characters")
            .Matches(@"^[A-ZА-ЯƏÜÖĞİŞÇ][A-ZА-ЯƏÜÖĞİŞÇa-zа-яəüöğışç0-9\s'""':;,.\(\)\*\-_]{2,127}$")
                .WithMessage("Title field has invalid format")
            .WithErrorCode("InvalidTitleFormat");

            RuleFor(product => product.Description)
            .MaximumLength(500)
                .WithMessage("Description field exceeds maximum length of 500 characters")
            .WithErrorCode("InvalidDescriptionFormat");

            RuleFor(product => product.Price)
            .NotEmpty()
                .WithMessage("Price field is required")
            .WithErrorCode("InvalidPriceFormat");

            RuleFor(product => product.CurrencyId)
            .NotEmpty()
                .WithMessage("CurrencyId field is required")
            .Matches(@"^[0-9A-Fa-f]{8}[-]?([0-9A-Fa-f]{4}[-]?){3}[0-9A-Fa-f]{12}$")
                .WithMessage("CurrencyId field has invalid format")
            .WithErrorCode("InvalidCurrencyIdFormat");

            RuleFor(product => product.CategoryId)
            .NotEmpty()
            .WithMessage("CategoryId field is required")
            .Matches(@"^[0-9A-Fa-f]{8}[-]?([0-9A-Fa-f]{4}[-]?){3}[0-9A-Fa-f]{12}$")
                .WithMessage("CategoryId field has invalid format")
            .WithErrorCode("InvalidCategoryIdFormat");

            RuleFor(product => product.SubcategoryId)
            .NotEmpty()
            .WithMessage("SubcategoryId field is required")
            .Matches(@"^[0-9A-Fa-f]{8}[-]?([0-9A-Fa-f]{4}[-]?){3}[0-9A-Fa-f]{12}$")
                .WithMessage("SubcategoryId field has invalid format")
            .WithErrorCode("InvalidSubcategoryIdFormat");

            RuleFor(product => product.CityId)
            .NotEmpty()
            .WithMessage("CityId field is required")
            .Matches(@"^[0-9A-Fa-f]{8}[-]?([0-9A-Fa-f]{4}[-]?){3}[0-9A-Fa-f]{12}$")
                .WithMessage("CityId field has invalid format")
            .WithErrorCode("InvalidCityIdFormat");
        }
    }
}
