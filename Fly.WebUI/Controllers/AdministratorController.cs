using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Fly.WebUI.Controllers
{
    [Authorize(Policy = "AdministratorOnly")]
    public class AdministratorController : Controller
    {
        public async Task<IActionResult> Index()
        {
            return View();
        }
    }
}
