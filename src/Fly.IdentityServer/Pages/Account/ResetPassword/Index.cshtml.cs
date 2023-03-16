using Azure.Communication.Email.Models;
using Fly.Core.Entities;
using Fly.Core.Services;
using Fly.Shared.DataTransferObjects;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.WebUtilities;
using System.Text;

namespace Fly.IdentityServer.Pages.Account.ResetPassword
{
    public class IndexModel : PageModel
    {
        [BindProperty]
        public ResetPasswordDto ResetPasswordDto { get; set; }
        public string ErrorMessage { get; set; }

        private readonly UserManager<User> _userManager;
        private readonly IConfiguration _configuration;
        private readonly IEmailSender<EmailMessage> _emailSender;

        public IndexModel(
            IEmailSender<EmailMessage> emailSender,
            IConfiguration configuration,
            UserManager<User> userManager)
        {
            _configuration = configuration;
            _emailSender = emailSender;
            _userManager = userManager;
        }

        public void OnGet()
        {
        }

        public async Task<IActionResult> OnPost()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            var user = await _userManager.FindByNameAsync(ResetPasswordDto.UserName);

            if (user == null)
            {
                ErrorMessage = "User does not exist";
                return Page();
            }

            var token = await _userManager.GeneratePasswordResetTokenAsync(user);

            var bytes = Encoding.UTF8.GetBytes(token);
            var validToken = WebEncoders.Base64UrlEncode(bytes);

            var confirmationLink = "https://" + Request.Host.ToString() + $"/account/submitnewpassword?token={validToken}&userName={user.UserName}";

            var emailContent = new EmailContent("Reset Password");
            emailContent.PlainText = confirmationLink;
            var emails = new List<EmailAddress> { new EmailAddress(user.Email) };
            var emeilRecipients = new EmailRecipients(emails);
            var emailMessage = new EmailMessage(_configuration["SenderDomain"], emailContent, emeilRecipients);

            _emailSender.Send(emailMessage);

            return RedirectToPage("success");
        }
    }
}
