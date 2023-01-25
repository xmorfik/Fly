using Fly.Core.DataTransferObjects;
using Fly.Core.Pagination;
using Fly.Core.Parameters;
using Fly.Core.Services;
using Microsoft.AspNetCore.Mvc;

namespace Fly.WebAPI.Controllers;

[ApiController]
[Route("[controller]")]
public class HomeController : Controller
{
    private readonly IService<FlightDTO, PagedResponse<List<FlightDTO>>, FlightParameter> _service;

    public HomeController(IService<FlightDTO, PagedResponse<List<FlightDTO>>, FlightParameter> service)
    {
        _service = service;
    }

    [HttpGet]
    public async Task<IActionResult> Index([FromQuery] Page page)
    {
        return Ok(await _service.GetListAsync(new FlightParameter(),page));
    }
}
