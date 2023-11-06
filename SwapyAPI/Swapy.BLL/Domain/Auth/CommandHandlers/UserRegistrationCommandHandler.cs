using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Swapy.BLL.Domain.Auth.Commands;
using Swapy.BLL.Interfaces;
using Swapy.Common.Entities;
using Swapy.Common.Enums;
using Swapy.Common.Exceptions;

namespace Swapy.BLL.Domain.Auth.CommandHandlers
{
    public class UserRegistrationCommandHandler : IRequestHandler<UserRegistrationCommand, Unit>
    {
        private readonly IEmailService _emailService;
        private readonly UserManager<User> _userManager;
        private readonly IKeyVaultService _keyVaultService;

        public UserRegistrationCommandHandler(IEmailService emailService, UserManager<User> userManager, IKeyVaultService keyVaultService)
        {
            _emailService = emailService;
            _userManager = userManager;
            _keyVaultService = keyVaultService;
        }

        public async Task<Unit> Handle(UserRegistrationCommand request, CancellationToken cancellationToken)
        {
            var emailExists = await _userManager.FindByEmailAsync(request.Email);
            var phoneExists = await _userManager.Users.FirstOrDefaultAsync(u => u.PhoneNumber.Equals(request.PhoneNumber));

            if (emailExists != null || phoneExists != null) throw new EmailOrPhoneTakenException("Email or Phone already taken");

            var user = new User()
            {
                FirstName = request.FirstName,
                LastName = request.LastName,
                Email = request.Email,
                PhoneNumber = request.PhoneNumber,
                Type = UserType.Seller,
                IsSubscribed = true,
                HasUnreadMessages = false,
                Logo = "default-user-logo.png"
            };
            
            user.UserName = user.Id.Replace("-", "");
            var result = await _userManager.CreateAsync(user, request.Password);
            if (!result.Succeeded) throw new InvalidOperationException("User creation failed");
            
            var confirmationToken = await _userManager.GenerateEmailConfirmationTokenAsync(user);
            var webUrl = await _keyVaultService.GetSecretValue("Web-Url");
            var callbackUrl = new UriBuilder(webUrl);
            callbackUrl.Path = "/auth/verify-email";
            callbackUrl.Query = $"userid={user.Id}&token={confirmationToken}";
            await _emailService.SendConfirmationEmailAsync(user.Email, callbackUrl.Uri.ToString());

            return Unit.Value;
        }
    }
}
