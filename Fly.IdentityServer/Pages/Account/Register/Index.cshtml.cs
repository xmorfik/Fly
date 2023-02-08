using Fly.Core.DataTransferObjects;
using Fly.Core.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Fly.IdentityServer.Pages.Account.Register
{
    public class IndexModel : PageModel
    {
        [BindProperty]
        public UserForRegistrationDto UserForRegistrationDto { get; set; }
        private readonly UserManager<User> _userManager;
        private readonly SignInManager<User> _signInManager;

        public IndexModel(UserManager<User> userManager, SignInManager<User> signInManager)
        {
            _signInManager = signInManager;
            _userManager = userManager;
        }

        public async Task<IActionResult> OnPost(UserForRegistrationDto userForRegistrationDto)
        {
            var user = new User();
            user.UserName = UserForRegistrationDto.UserName;
            var result = await _userManager.CreateAsync(user, UserForRegistrationDto.Password);

            if (result.Succeeded)
            {
                await _signInManager.SignInAsync(user, false);

                return Redirect("/");
            }
            return Page();
        }
    }
}
