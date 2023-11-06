using FluentValidation;
using Swapy.Common.DTO.Items.Requests.Commands;

namespace Swapy.API.Validators
{
    public class UpdateItemAttributeValidator : AbstractValidator<UpdateItemAttributeCommandDTO>
    {
        public UpdateItemAttributeValidator()
        {
            RuleFor(item => item.ItemTypeId)
            .NotEmpty()
                .WithMessage("ItemTypeId field is required")
            .Matches(@"^[0-9A-Fa-f]{8}[-]?([0-9A-Fa-f]{4}[-]?){3}[0-9A-Fa-f]{12}$")
                .WithMessage("ItemTypeId field has invalid format")
            .WithErrorCode("InvalidItemTypeIdFormat");
        }
    }
}
