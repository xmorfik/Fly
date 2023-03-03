using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;

namespace Fly.WebUI.Controllers;

public class AccountController : Controller
{
    private readonly AuthorityUri authority;

    public AccountController(IOptions<AuthorityUri> options) 
    {
        authority = options.Value;
    }

    public IActionResult Index()
    {
        return View();
    }

    public async Task<IActionResult> Login()
    {
        return Challenge(new AuthenticationProperties
        {
            RedirectUri = "/"
        });
    }

    public async Task<IActionResult> Logout()
    {
        await HttpContext.SignOutAsync("Cookie");
        await HttpContext.SignOutAsync("oidc");

        return Redirect("/"); ;
    }

    public IActionResult Register()
    {
        return Redirect(authority.Uri +"account/register");
    }
}
