using FluentValidation;
using Swapy.Common.DTO.TVs.Requests.Commands;

namespace Swapy.API.Validators
{
    public class UpdateTVAttributeValidator : AbstractValidator<UpdateTVAttributeCommandDTO>
    {
        public UpdateTVAttributeValidator()
        {
            RuleFor(tv => tv.TvTypeId)
            .NotEmpty()
                .WithMessage("TVTypeId field is required")
            .Matches(@"^[0-9A-Fa-f]{8}[-]?([0-9A-Fa-f]{4}[-]?){3}[0-9A-Fa-f]{12}$")
                .WithMessage("TVTypeId field has invalid format")
            .WithErrorCode("InvalidTVTypeIdFormat");

            RuleFor(tv => tv.TvBrandId)
            .NotEmpty()
                .WithMessage("TVBrandId field is required")
            .Matches(@"^[0-9A-Fa-f]{8}[-]?([0-9A-Fa-f]{4}[-]?){3}[0-9A-Fa-f]{12}$")
                .WithMessage("TVBrandId field has invalid format")
            .WithErrorCode("InvalidTVBrandIdFormat");

            RuleFor(tv => tv.ScreenResolutionId)
            .NotEmpty()
                .WithMessage("ScreenResolutionId field is required")
            .Matches(@"^[0-9A-Fa-f]{8}[-]?([0-9A-Fa-f]{4}[-]?){3}[0-9A-Fa-f]{12}$")
                .WithMessage("ScreenResolutionId field has invalid format")
            .WithErrorCode("InvalidScreenResolutionIdFormat");

            RuleFor(tv => tv.ScreenDiagonalId)
            .NotEmpty()
                .WithMessage("ScreenDiagonalId field is required")
            .Matches(@"^[0-9A-Fa-f]{8}[-]?([0-9A-Fa-f]{4}[-]?){3}[0-9A-Fa-f]{12}$")
                .WithMessage("ScreenDiagonalId field has invalid format")
            .WithErrorCode("InvalidScreenDiagonalIdFormat");
        }
    }
}
