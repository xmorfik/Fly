using Fly.Core.Entities;
using Fly.Core.Parameters;
using Fly.Core.Services;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;

namespace Fly.WebUI.Controllers;

public class AccountController : Controller
{
    private readonly IService<Aircraft, AircraftParameter> _service;
    public AccountController(IService<Aircraft, AircraftParameter> service)
    {
        _service = service;
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

    public IActionResult Register()
    {
        return Redirect("");
    }
}
