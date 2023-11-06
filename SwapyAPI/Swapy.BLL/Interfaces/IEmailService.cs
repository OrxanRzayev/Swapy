using Swapy.Common.Enums;

namespace Swapy.BLL.Interfaces
{
    public interface IEmailService
    {
        Task SendEmailAsync(string email, string subject, string message);
        Task SendConfirmationEmailAsync(string email, string callbackUrl);
        Task SendPasswordChangedSuccessfullyAsync(string email);
        Task SendForgotPasswordAsync(string email, string callbackUrl);
        Task SendResetPasswordAsync(string email);
        Task SendTryRemoveUserAsync(string email, string callbackUrl);
        Task SendRemovedAsync(string email);
        Task SendNewProductNotificationAsync(string email, string sellerName, string title, string description, string city, decimal price, string currency, string productId);
        Task SendUnreadMessageAsync(string email, string senderName, ChatType type);
    }
}
