using Fly.Core.DataTransferObjects;
using Fly.Core.Services;
using Microsoft.AspNetCore.Mvc;

namespace Fly.WebAPI.Controllers;

[ApiController]
[Route("[controller]")]
public class HomeController : Controller
{
    private readonly IService<FlightDTO> _service;

    public HomeController(IService<FlightDTO> service)
    {
        _service = service;
    }

    [HttpGet]
    public async Task<IActionResult> Index()
    {
        return Ok();
    }
}
