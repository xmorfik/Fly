using Fly.Core.Entities;
using Fly.WebUI.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Security.Claims;

namespace Fly.IdentityServer.Pages.Account.Register
{
    [Authorize(Roles = "Administrator")]
    public class ManagerModel : PageModel
    {
        [BindProperty]
        public CreateManagerDto CreateManagerDto { get; set; }
        private readonly UserManager<User> _userManager;

        public ManagerModel(UserManager<User> userManager)
        {;
            _userManager = userManager;
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

            var user = new User();
            user.UserName = CreateManagerDto.UserName;
            var result = await _userManager.CreateAsync(user, CreateManagerDto.Password);

            if (result.Succeeded)
            {
                await _userManager.AddClaimAsync(user, new Claim("Role", "Manager"));
                await _userManager.AddClaimAsync(user, new Claim("Airline", CreateManagerDto.AirlineId));

                return Redirect("https://localhost:5002");
            }
            return Page();
        }
    }
}
