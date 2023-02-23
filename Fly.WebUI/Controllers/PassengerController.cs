using Microsoft.AspNetCore.Mvc;

namespace Fly.WebUI.Controllers
{
    public class PassengerController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
