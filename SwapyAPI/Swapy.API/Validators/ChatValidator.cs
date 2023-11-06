using FluentValidation;
using Swapy.Common.DTO.Chats.Requests;

namespace Swapy.API.Validators
{
    public class ChatValidator : AbstractValidator<CreateChatCommandDTO>
    {
        public ChatValidator()
        {
            RuleFor(chat => chat.ProductId)
            .NotEmpty()
                .WithMessage("ProductId field is required")
            .Matches(@"^[0-9A-Fa-f]{8}[-]?([0-9A-Fa-f]{4}[-]?){3}[0-9A-Fa-f]{12}$")
                .WithMessage("ProductId field has invalid format")
            .WithErrorCode("InvalidProductIdFormat");
        }
    }
}
