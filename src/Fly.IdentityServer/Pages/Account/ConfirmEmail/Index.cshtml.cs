using Fly.Core.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.WebUtilities;
using Microsoft.Extensions.Options;
using System.Text;

namespace Fly.IdentityServer.Pages.Account.ConfirmEmail
{
    public class IndexModel : PageModel
    {
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

        public async Task<IActionResult> OnGet(string token, string email)
        {
            if (token == null)
            {
                return Redirect("/home/error");
            }

            var bytes = WebEncoders.Base64UrlDecode(token);
            var validToken = Encoding.UTF8.GetString(bytes);

            var user = await _userManager.FindByEmailAsync(email);
            if (user == null)
            {
                return Redirect("/home/error");
            }

            var result = await _userManager.ConfirmEmailAsync(user, validToken);
            if (result.Succeeded)
            {
                return RedirectToPage("success");
            }

            return Redirect("/home/error");
        }
    }
}
