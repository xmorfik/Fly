using Fly.Core.Entities;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Options;

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

        public async Task<IActionResult> OnGet()
        {
            
            return Page();
        }
    }
}
