using Fly.Core.DataTransferObjects;
using Fly.Core.Parameters;
using Fly.Core.Services;
using Microsoft.AspNetCore.Mvc;

namespace Fly.WebAPI.Controllers;

[ApiController]
[Route("[controller]")]
public class HomeController : Controller
{
    private readonly IService<FlightDTO> _service;
    private readonly IFilter<FlightDTO, FlightParameter> _filter;

    public HomeController(IService<FlightDTO> service, 
        IFilter<FlightDTO, FlightParameter> filter)
    {
        _service = service;
        _filter = filter;
    }

    [HttpGet]
    public async Task<IActionResult> Index()
    {
        return Ok();
    }
}
