using Fly.Core.Entities;
using Fly.Shared.DataTransferObjects;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Security.Claims;

namespace Fly.IdentityServer.Pages.Account.Register;

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

    public async Task<IActionResult> OnGet()
    {
        await _signInManager.SignOutAsync();
        return Page();
    }

    public async Task<IActionResult> OnPost()
    {
        if (!ModelState.IsValid)
        {
            return Page();
        }

        var user = new User();
        user.UserName = UserForRegistrationDto.UserName;
        var result = await _userManager.CreateAsync(user, UserForRegistrationDto.Password);

        if (result.Succeeded)
        {
            await _signInManager.SignInAsync(user, false);
            await _userManager.AddClaimAsync(user, new Claim("Role", "Passenger"));

            return Redirect("https://localhost:5002");
        }
        return Page();
    }
}
