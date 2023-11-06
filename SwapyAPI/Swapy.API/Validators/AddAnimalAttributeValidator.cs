using FluentValidation;
using Swapy.Common.DTO.Animals.Requests.Commands;

namespace Swapy.API.Validators
{
    public class AddAnimalAttributeValidator : AbstractValidator<AddAnimalAttributeCommandDTO>
    {
        public AddAnimalAttributeValidator()
        {
            RuleFor(animal => animal.AnimalBreedId)
            .NotEmpty()
                .WithMessage("AnimalBreedId field is required")
            .Matches(@"^[0-9A-Fa-f]{8}[-]?([0-9A-Fa-f]{4}[-]?){3}[0-9A-Fa-f]{12}$")
                .WithMessage("AnimalBreedId field has invalid format")
            .WithErrorCode("InvalidAnimalBreedIdFormat");
        }
    }
}
