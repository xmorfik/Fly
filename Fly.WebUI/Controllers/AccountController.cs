using Fly.Core.Entities;
using Fly.Core.Pagination;
using Fly.Core.Parameters;
using Fly.Core.Services;
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

    public IActionResult Login()
    {
        _service.GetListAsync(new AircraftParameter(), new Page());
        return View();
    }

    public IActionResult Register()
    {
        return View();
    }
}
