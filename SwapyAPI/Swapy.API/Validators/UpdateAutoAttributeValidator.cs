using FluentValidation;
using Swapy.Common.DTO.Autos.Requests.Commands;
using Swapy.Common.Entities;

namespace Swapy.API.Validators
{
    public class UpdateAutoAttributeValidator : AbstractValidator<UpdateAutoAttributeCommandDTO>
    {
        public UpdateAutoAttributeValidator()
        {
            RuleFor(auto => auto.Miliage)
            .GreaterThan(-1)
                .WithMessage("Miliage field must be non-negative")
            .WithErrorCode("InvalidMiliageFormat");

            RuleFor(auto => auto.EngineCapacity)
            .GreaterThan(-1)
                .WithMessage("EngineCapacity field must be non-negative")
            .LessThanOrEqualTo(18200)
                .WithMessage("EngineCapacity field must be less than or equal to 18200")
            .WithErrorCode("InvalidEngineCapacityFormat");

            RuleFor(auto => auto.ReleaseYear)
            .NotEmpty()
                .WithMessage("ReleaseYear field is required")
            .WithErrorCode("InvalidReleaseYearFormat");

            RuleFor(auto => auto.FuelTypeId)
            .NotEmpty()
                .WithMessage("FuelTypeId field is required")
            .Matches(@"^[0-9A-Fa-f]{8}[-]?([0-9A-Fa-f]{4}[-]?){3}[0-9A-Fa-f]{12}$")
                .WithMessage("FuelTypeId field has invalid format")
            .WithErrorCode("InvalidFuelTypeIdFormat");

            RuleFor(auto => auto.AutoColorId)
            .NotEmpty()
                .WithMessage("AutoColorId field is required")
            .Matches(@"^[0-9A-Fa-f]{8}[-]?([0-9A-Fa-f]{4}[-]?){3}[0-9A-Fa-f]{12}$")
                .WithMessage("AutoColorId field has invalid format")
            .WithErrorCode("InvalidAutoColorIdFormat");

            RuleFor(auto => auto.TransmissionTypeId)
            .NotEmpty()
                .WithMessage("TransmissionTypeId field is required")
            .Matches(@"^[0-9A-Fa-f]{8}[-]?([0-9A-Fa-f]{4}[-]?){3}[0-9A-Fa-f]{12}$")
                .WithMessage("TransmissionTypeId field has invalid format")
            .WithErrorCode("InvalidTransmissionTypeIdFormat");

            RuleFor(auto => auto.AutoModelId)
            .NotEmpty()
                .WithMessage("AutoModelId field is required")
            .Matches(@"^[0-9A-Fa-f]{8}[-]?([0-9A-Fa-f]{4}[-]?){3}[0-9A-Fa-f]{12}$")
                .WithMessage("AutoModelId field has invalid format")
            .WithErrorCode("InvalidAutoModelIdFormat");
        }
    }
}
