using FluentValidation;
using Swapy.Common.DTO.Electronics.Requests.Commands;

namespace Swapy.API.Validators
{
    public class AddElectronicsAttributeValidator : AbstractValidator<AddElectronicAttributeCommandDTO>
    {
        public AddElectronicsAttributeValidator()
        {
            RuleFor(electrionics => electrionics.MemoryModelId)
            .NotEmpty()
                .WithMessage("MemoryModelId field is required")
            .Matches(@"^[0-9A-Fa-f]{8}[-]?([0-9A-Fa-f]{4}[-]?){3}[0-9A-Fa-f]{12}$")
                .WithMessage("MemoryModelId field has invalid format")
            .WithErrorCode("InvalidMemoryModelIdFormat");

            RuleFor(electrionics => electrionics.ModelColorId)
            .NotEmpty()
                .WithMessage("ModelColorId field is required")
            .Matches(@"^[0-9A-Fa-f]{8}[-]?([0-9A-Fa-f]{4}[-]?){3}[0-9A-Fa-f]{12}$")
                .WithMessage("ModelColorId field has invalid format")
            .WithErrorCode("InvalidModelColorIdFormat");
        }
    }
}
