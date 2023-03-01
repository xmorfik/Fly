using Fly.Core.Entities;
using Fly.Core.Pagination;
using Fly.Core.Parameters;
using Fly.Core.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace Fly.WebUI.Controllers;

[Authorize( Roles ="Passenger")]
public class PassengerController : Controller
{
    public readonly IService<Passenger, PassengerParameter> _service;

    public PassengerController(IService<Passenger, PassengerParameter> service)
    {
        _service = service;
    }

    public IActionResult Index()
    {
        return View();
    }

    [HttpGet]
    public async Task<IActionResult> Profile()
    {
        var userId = User.FindFirstValue("sub");
        var user = await _service.GetListAsync(new PassengerParameter { UserId = userId}, new Page());
        return View(user.FirstOrDefault());
    }

    [HttpPost]
    public async Task<IActionResult> Profile(Passenger passenger)
    {
        await _service.UpdateAsync(passenger);
        return Redirect("/passenger");
    }
}
