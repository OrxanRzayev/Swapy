using Microsoft.Extensions.Configuration;
using Swapy.BLL.Interfaces;
using Swapy.Common.Enums;
using System.Net;
using System.Net.Mail;

namespace Swapy.BLL.Services
{
    public class EmailService : IEmailService
    {
        private readonly IConfiguration _configuration;
        private readonly IKeyVaultService _keyVaultService;

        public EmailService(IConfiguration configuration, IKeyVaultService keyVaultService)
        {
            _configuration = configuration;
            _keyVaultService = keyVaultService;
        }

        public async Task SendEmailAsync(string email, string subject, string message)
        {
            try
            {
                var client = new MailMessage();
                client.From = new MailAddress(_configuration["Email-Domain"], "Swapy");
                client.Subject = subject;
                client.To.Add(new MailAddress(email));
                client.Body = $"<html><body>{message}</body></html>";
                client.IsBodyHtml = true;

                var token = await _keyVaultService.GetSecretValue("Email-Token");
                var smptClient = new SmtpClient("smtp.gmail.com")
                {
                    Port = 587,
                    Credentials = new NetworkCredential(_configuration["Email-Domain"], token),
                    EnableSsl = true
                };

                smptClient.Send(client);
            }
            catch(Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task SendConfirmationEmailAsync(string email, string callbackUrl)
        {
            string subject = "Registration confirmation";
            string message = @$"<div style='display:block; font-size:1.2rem; color:black;'>
                                    <div style='display:flex;'>
                                        <h1 style='color:#3a5a3d;'>Swapy</h1>
                                        <img style='height:3.5rem; margin:auto 0' src='{_configuration["LogoUrl"]}'>
                                    </div>
                                    <p style='font-weight:bold; font-size:1.4rem;'>Thank you for joining Swapy!</p>
                                    <p style='margin-bottom:2rem;'>Click the button below to confirm email address:</p>
                                    <a href='{callbackUrl}' style='width:12rem; font-size:1.15rem; text-decoration:none; border:none; padding:1rem; border-radius:0.5rem; color:#ffffff; margin-bottom:1.5rem; background-color:#3a5a3d;'>Confirm email address</a>
                                    <div style='margin-top:2rem;'>                                    
                                        <p style='margin:0;'>Welcome and thanks,</p>
                                        <p style='margin:0;'>The Swapy Team</p>
                                        <small>© 2023 Swapy</small>
                                    </div>                                    
                                </div>";

            await SendEmailAsync(email, subject, message);
        }

        public async Task SendPasswordChangedSuccessfullyAsync(string email)
        {
            string subject = "Password changed successfully";
            string message = @$"<div style='display:block; font-size:1.2rem; color:black;'>
                                    <div style='display:flex;'>
                                        <h1 style='color:#3a5a3d;'>Swapy</h1>
                                        <img style='height:3.5rem; margin:auto 0' src='{_configuration["LogoUrl"]}'>
                                    </div>
                                    <p style='font-weight:bold; font-size:1.4rem;'>Your password has been successfully changed.</p>
                                    <p>If you have not completed this action, please contact us immediately.</p>
                                    <div style='margin-top:2rem;'>                                    
                                        <p style='margin:0;'>Best wishes,</p>
                                        <p style='margin:0;'>The Swapy Team</p>
                                        <small>© 2023 Swapy</small>
                                    </div>                                    
                                </div>";

            await SendEmailAsync(email, subject, message);
        }

        public async Task SendForgotPasswordAsync(string email, string callbackUrl)
        {
            string subject = "Password recovery";
            string message = @$"<div style='display:block; font-size:1.2rem; color:black;'>
                                    <div style='display:flex;'>
                                        <h1 style='color:#3a5a3d;'>Swapy</h1>
                                        <img style='height:3.5rem; margin:auto 0' src='{_configuration["LogoUrl"]}'>
                                    </div>
                                    <p style='font-weight:bold; font-size:1.4rem;'>You have requested a password reset for your account.</p>
                                    <p style='margin-bottom:2rem;'>To complete the password reset process, please visit the following link:</p>
                                    <a href='{callbackUrl}' style='width:12rem; font-size:1.15rem; text-decoration:none; border:none; padding:1rem; border-radius:0.5rem; color:#ffffff; margin-bottom:1.5rem; background-color:#3a5a3d;'>Reset password</a>
                                    <p style='margin-top:2rem;'>If you did not request a password reset, please ignore this message.</p>
                                    <div>                                    
                                        <p style='margin:0;'>Best wishes,</p>
                                        <p style='margin:0;'>The Swapy Team</p>
                                        <small>© 2023 Swapy</small>
                                    </div>                                    
                                </div>";

            await SendEmailAsync(email, subject, message);
        }

        public async Task SendResetPasswordAsync(string email)
        {
            string subject = "Password changed successfully";
            string message = @$"<div style='display:block; font-size:1.2rem; color:black;'>
                                    <div style='display:flex;'>
                                        <h1 style='color:#3a5a3d;'>Swapy</h1>
                                        <img style='height:3.5rem; margin:auto 0' src='{_configuration["LogoUrl"]}'>
                                    </div>
                                    <p style='font-weight:bold; font-size:1.4rem;'>Your password has been successfully reset.</p>
                                    <p>If you have not completed this action, please contact us immediately.</p>
                                    <div style='margin-top:2rem;'>                                    
                                        <p style='margin:0;'>Best wishes,</p>
                                        <p style='margin:0;'>The Swapy Team</p>
                                        <small>© 2023 Swapy</small>
                                    </div>                                    
                                </div>";

            await SendEmailAsync(email, subject, message);
        }

        public async Task SendTryRemoveUserAsync(string email, string callbackUrl)
        {
            string subject = "Account deleting";
            string message = @$"<div style='display:block; font-size:1.2rem; color:black;'>
                                    <div style='display:flex;'>
                                        <h1 style='color:#3a5a3d;'>Swapy</h1>
                                        <img style='height:3.5rem; margin:auto 0' src='{_configuration["LogoUrl"]}'>
                                    </div>
                                    <p style='font-weight:bold; font-size:1.4rem;'>We have received a request to delete your account. If you have not initiated this request, please ignore this email. Otherwise, read further instructions below.</p>
                                    <p style='font-weight:bold; font-size:1.4rem;'>We want to warn you that deleting your account will result in the permanent loss of all your data and access to our service. Please note that after deleting your account, you will no longer be able to log in with your current credentials.</p>
                                    <p style='margin-bottom:2rem;'>If you really wish to delete your account, please confirm this by clicking on the following link:</p>
                                    <a href='{callbackUrl}' style='width:12rem; font-size:1.15rem; text-decoration:none; border:none; padding:1rem; border-radius:0.5rem; color:#ffffff; margin-bottom:1.5rem; background-color:#3a5a3d;'>Delete account</a>
                                    <p style='margin-top:2rem;'>If you do not want to delete your account, do not follow the link and ignore this email.</p>
                                    <div>                                    
                                        <p style='margin:0;'>Best wishes,</p>
                                        <p style='margin:0;'>The Swapy Team</p>
                                        <small>© 2023 Swapy</small>
                                    </div>                                    
                                </div>";

            await SendEmailAsync(email, subject, message);
        }

        public async Task SendRemovedAsync(string email)
        {
            string subject = "Account deleted";
            string message = @$"<div style='display:block; font-size:1.2rem; color:black;'>
                                    <div style='display:flex;'>
                                        <h1 style='color:#3a5a3d;'>Swapy</h1>
                                        <img style='height:3.5rem; margin:auto 0' src='{_configuration["LogoUrl"]}'>
                                    </div>
                                    <p style='font-weight:bold; font-size:1.4rem;'>We wanted to inform you that your account has been deleted. All your data and related information has been permanently deleted from our system.</p>
                                    <p>If you have not initiated account deletion, please contact our support team for more information.</p>
                                    <div>                                    
                                        <p style='margin:0;'>Best wishes,</p>
                                        <p style='margin:0;'>The Swapy Team</p>
                                        <small>© 2023 Swapy</small>
                                    </div>                                    
                                </div>";

            await SendEmailAsync(email, subject, message);
        }

        public async Task SendUnreadMessageAsync(string email, string senderName, ChatType type)
        {
            string subject = $"Unread Message from {senderName} in {type} Chat";
            string message = @$"<div style='display:block; font-size:1.2rem; color:black;'>
                                    <div style='display:flex;'>
                                        <h1 style='color:#3a5a3d;'>Swapy</h1>
                                        <img style='height:3.5rem; margin:auto 0' src='{_configuration["LogoUrl"]}'>
                                    </div>
                                    <p style='font-weight:bold; font-size:1.4rem;'>We wanted to inform you that you have an unread message in your chat.</p>
                                    <p>Please log in to your account to read the message and respond if necessary.</p>
                                    <div>                                    
                                        <p style='margin:0;'>Best wishes,</p>
                                        <p style='margin:0;'>The Swapy Team</p>
                                        <small>© 2023 Swapy</small>
                                    </div>                                    
                                </div>";

            await SendEmailAsync(email, subject, message);
        }

        public async Task SendNewProductNotificationAsync(string email, string sellerName, string title, string description, string city, decimal price, string currency, string productId)
        {
            string subject = $"New product from \"{sellerName}\"";
            string message = @$"<div style='display:block; font-size:1.2rem; color:black;'>
                                    <div style='display:flex;'>
                                        <h1 style='color:#3a5a3d;'>Swapy</h1>
                                        <img style='height:3.5rem; margin:auto 0' src='{_configuration["LogoUrl"]}'>
                                    </div>
                                    <p style='font-weight:bold; font-size:1.4rem;'>{sellerName} published a new product.</p>
                                    <div style='margin-bottom:2rem;'>
                                        <table style='margin-bottom: 1.5rem'>
                                            <tr>
                                                <th>Product details</th>
                                            </tr>
                                            <tr>
                                                <td>Title:</td>
                                                <td>{title}</td>
                                            </tr>
                                            <tr>
                                                <td>Description:</td>
                                                <td>{description}</td>
                                            </tr>
                                            <tr>
                                                <td>City:</td>
                                                <td>{city}</td>
                                            </tr>
                                            <tr>
                                                <td>Price:</td>
                                                <td>{price} {currency}</td>
                                            </tr>
                                        </table>
                                        <a href='{_configuration["WebUrl"]}/products/{productId}' style='width:12rem; font-size:1.15rem; text-decoration:none; border:none; padding:1rem; border-radius:0.5rem; color:#ffffff; background-color:#3a5a3d;'>View product</a>
                                    </div>
                                    <p>Don't miss your chance to take advantage of these new features. Visit our site for more information and purchase.</p>
                                    <div style='margin-top: 1rem;'>
                                        <p style='margin:0;'>Best wishes,</p>
                                        <p style='margin:0;'>The Swapy Team</p>
                                        <small>© 2023 Swapy</small>
                                    </div>
                                    <p style='font-size: 0.8rem'>If you no longer wish to receive such notifications, you can <a href='{_configuration["WebUrl"]}/users/settings'>unsubscribe</a></p>
                                </div>";

            await SendEmailAsync(email, subject, message);
        }
    }
}
