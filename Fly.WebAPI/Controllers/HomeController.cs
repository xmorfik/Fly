using Fly.Core.Entities;
using Fly.Core.Pagination;
using Fly.Core.Parameters;
using Fly.Core.Services;
using Microsoft.AspNetCore.Mvc;

namespace Fly.WebAPI.Controllers;

[ApiController]
[Route("[controller]")]
public class HomeController : Controller
{
    private readonly IService<Flight, FlightParameter> _service;

    public HomeController(IService<Flight, FlightParameter> service)
    {
        _service = service;
    }

    [HttpGet]
    public async Task<IActionResult> Index([FromQuery] Page page)
    {
        return Ok(await _service.GetAsync(1));
    }
}
