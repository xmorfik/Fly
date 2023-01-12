using Microsoft.AspNetCore.Mvc;

namespace Fly.WebAPI.Controllers;

[ApiController]
[Route("[controller]")]
public class HomeController : Controller
{
    [HttpGet]
    public IActionResult Index()
    {
        return Ok();
    }
}
