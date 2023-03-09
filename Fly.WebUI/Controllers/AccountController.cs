using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;

namespace Fly.WebUI.Controllers;

[AllowAnonymous]
public class AccountController : Controller
{
    private readonly AuthorityUri _authority;

    public AccountController(IOptions<AuthorityUri> options)
    {
        _authority = options.Value;
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
        return Redirect(_authority.Uri + "account/logout");
    }

    public IActionResult Register()
    {
        return Redirect(_authority.Uri + "account/register");
    }
}
