using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;

namespace Fly.WebUI.Controllers;

public class AccountController : Controller
{

    public AccountController()
    {
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
        return Redirect("https://localhost:5004/account/register");
    }
}
