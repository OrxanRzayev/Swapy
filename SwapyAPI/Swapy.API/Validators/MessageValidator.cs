using FluentValidation;
using Swapy.Common.DTO.Chats.Requests;

namespace Swapy.API.Validators
{
    public class MessageValidator : AbstractValidator<SendMessageCommandDTO>
    {
        public MessageValidator()
        {
            RuleFor(message => message.Text)
            .MinimumLength(1)
                .WithMessage("Text field should have a minimum length of 1 characters")
            .MaximumLength(300)
                .WithMessage("Text field exceeds maximum length of 300 characters")
            .WithErrorCode("InvalidTextFormat");

            RuleFor(message => message.ChatId)
            .NotEmpty()
                .WithMessage("ChatId field is required")
            .Matches(@"^[0-9A-Fa-f]{8}[-]?([0-9A-Fa-f]{4}[-]?){3}[0-9A-Fa-f]{12}$")
                .WithMessage("ChatId field has invalid format")
            .WithErrorCode("InvalidChatIdFormat");
        }
    }
}
