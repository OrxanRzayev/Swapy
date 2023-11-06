using FluentValidation;
using Swapy.Common.DTO.Clothes.Requests.Commands;

namespace Swapy.API.Validators
{
    public class UpdateClothesAttributeValidator : AbstractValidator<UpdateClothesAttributeCommandDTO>
    {
        public UpdateClothesAttributeValidator()
        {
            RuleFor(clothes => clothes.ClothesSeasonId)
            .NotEmpty()
                .WithMessage("ClothesSeasonId field is required")
            .Matches(@"^[0-9A-Fa-f]{8}[-]?([0-9A-Fa-f]{4}[-]?){3}[0-9A-Fa-f]{12}$")
                .WithMessage("ClothesSeasonId field has invalid format")
            .WithErrorCode("InvalidClothesSeasonIdFormat");

            RuleFor(clothes => clothes.ClothesSizeId)
            .NotEmpty()
                .WithMessage("ClothesSizeId field is required")
            .Matches(@"^[0-9A-Fa-f]{8}[-]?([0-9A-Fa-f]{4}[-]?){3}[0-9A-Fa-f]{12}$")
                .WithMessage("ClothesSizeId field has invalid format")
            .WithErrorCode("InvalidClothesSizeIdFormat");

            RuleFor(clothes => clothes.ClothesBrandViewId)
            .NotEmpty()
                .WithMessage("ClothesBrandViewId field is required")
            .Matches(@"^[0-9A-Fa-f]{8}[-]?([0-9A-Fa-f]{4}[-]?){3}[0-9A-Fa-f]{12}$")
                .WithMessage("ClothesBrandViewId field has invalid format")
            .WithErrorCode("InvalidClothesBrandViewIdFormat");
        }
    }
}
