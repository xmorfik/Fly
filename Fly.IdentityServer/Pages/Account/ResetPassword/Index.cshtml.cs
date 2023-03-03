using Fly.Core.Entities;
using Fly.Shared.DataTransferObjects;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Options;
using Serilog;
using System.Security.Claims;

namespace Fly.IdentityServer.Pages.Account.PasswordReset
{
    public class IndexModel : PageModel
    {
        [BindProperty]
        public ResetPasswordDto ResetPasswordDto { get; set; }
        [BindProperty]
        public string Message { get; set; }

        private readonly UserManager<User> _userManager;
        private readonly SignInManager<User> _signInManager;
        private readonly ClientUriConfiguration _uriConfiguration;

        public IndexModel(
            UserManager<User> userManager,
            SignInManager<User> signInManager,
            IOptions<ClientUriConfiguration> clientUri)
        {
            _signInManager = signInManager;
            _userManager = userManager;
            _uriConfiguration = clientUri.Value;
        }

        public async Task<IActionResult> OnGet()
        {
            return Page();
        }

        public async Task<IActionResult> OnPost()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            var user = await _userManager.FindByNameAsync(ResetPasswordDto.UserName);
            if(user == null)
            {
                Message = "User does not exist";
                return Page();
            }

            var result = await _userManager.ChangePasswordAsync(user, ResetPasswordDto.Password, ResetPasswordDto.NewPassword);
            if (result.Succeeded)
            {
                return Redirect(_uriConfiguration.Uri);
            }

            return Page();
        }
    }
}
