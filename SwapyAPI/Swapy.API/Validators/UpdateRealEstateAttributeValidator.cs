using FluentValidation;
using Swapy.Common.DTO.RealEstates.Requests.Commands;

namespace Swapy.API.Validators
{
    public class UpdateRealEstateAttributeValidator : AbstractValidator<UpdateRealEstateAttributeCommandDTO>
    {
        public UpdateRealEstateAttributeValidator()
        {
            RuleFor(realEstate => realEstate.Area)
           .NotEmpty()
               .WithMessage("Area field is required")
           .GreaterThan(1)
               .WithMessage("Area field must be non-negative or null")
           .WithErrorCode("InvalidAreaFormat");

            RuleFor(realEstate => realEstate.RealEstateTypeId)
            .NotEmpty()
                .WithMessage("RealEstateTypeId field is required")
            .Matches(@"^[0-9A-Fa-f]{8}[-]?([0-9A-Fa-f]{4}[-]?){3}[0-9A-Fa-f]{12}$")
                .WithMessage("RealEstateTypeId field has invalid format")
            .WithErrorCode("InvalidRealEstateTypeIdFormat");
        }
    }
}
