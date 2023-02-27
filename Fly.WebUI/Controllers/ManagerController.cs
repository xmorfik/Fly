using Microsoft.AspNetCore.Mvc;

namespace Fly.WebUI.Controllers;

public class ManagerController : Controller
{
    public IActionResult Index()
    {
        return View();
    }
}
