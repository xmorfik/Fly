using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Fly.WebUI.Controllers;

[Authorize(Roles = "Manager")]
public class ManagerController : Controller
{
    public IActionResult Index()
    {
        return View();
    }
}
