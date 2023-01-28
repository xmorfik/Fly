using Fly.Core.Entities;
using Fly.Core.Pagination;
using Fly.Core.Parameters;
using Fly.Core.Services;
using Microsoft.AspNetCore.Mvc;

namespace Fly.WebAPI.Controllers;

[Route("api/[controller]")]
[ApiController]
public class AircraftsController : ControllerBase
{
    private readonly IService<Aircraft, AircraftParameter> _service;

    public AircraftsController(IService<Aircraft, AircraftParameter> service)
    {
        _service = service;
    }

    [HttpGet]
    public async Task<PagedResponse<ICollection<Aircraft>>> Get([FromQuery] AircraftParameter parameter, [FromQuery] Page page)
    {
        return await _service.GetListAsync(parameter, page);
    }

    [HttpGet("{id}")]
    public async Task<Response<Aircraft>> Get(int id)
    {
        return await _service.GetAsync(id);
    }

    [HttpPost]
    public async Task Post([FromBody] Aircraft value)
    {
        await _service.CreateAsync(value);
    }

    [HttpPut]
    public async Task Put([FromBody] Aircraft value)
    {
        await _service.UpdateAsync(value);
    }

    [HttpDelete("{id}")]
    public async Task Delete(int id)
    {
        await _service.DeleteAsync(id);
    }
}
