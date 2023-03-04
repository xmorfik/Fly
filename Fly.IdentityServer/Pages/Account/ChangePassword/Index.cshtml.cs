using Fly.Core.Entities;
using Fly.Shared.DataTransferObjects;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Options;

namespace Fly.IdentityServer.Pages.Account.ChangePassword
{
    public class IndexModel : PageModel
    {
        [BindProperty]
        public ChangePasswordDto ChangePasswordDto { get; set; }
        public string ErrorMessage { get; set; }

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

            var user = await _userManager.FindByNameAsync(ChangePasswordDto.UserName);
            if (user == null)
            {
                ErrorMessage = "User does not exist";
                return Page();
            }

            var result = await _userManager.ChangePasswordAsync(user, ChangePasswordDto.Password, ChangePasswordDto.NewPassword);
            if (result.Succeeded)
            {
                return Redirect(_uriConfiguration.Uri);
            }

            ErrorMessage = result.Errors.FirstOrDefault().Description;

            return Page();
        }
    }
}
