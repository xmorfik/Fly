using Fly.Core.Entities;
using Fly.Core.Pagination;
using Fly.Core.Parameters;
using Fly.Core.Services;
using Microsoft.AspNetCore.Mvc;

namespace Fly.WebAPI.Controllers;

[Route("api/[controller]")]
[ApiController]
public class AirlinesController : ControllerBase
{
    private readonly IService<Airline, AirlineParameter> _service;

    public AirlinesController(IService<Airline, AirlineParameter> service)
    {
        _service = service;
    }

    [HttpGet]
    public async Task<PagedResponse<ICollection<Airline>>> Get([FromQuery] AirlineParameter parameter, [FromQuery] Page page)
    {
        return await _service.GetListAsync(parameter, page);
    }

    [HttpGet("{id}")]
    public async Task<Response<Airline>> Get(int id)
    {
        return await _service.GetAsync(id);
    }

    [HttpPost]
    public async Task Post([FromBody] Airline value)
    {
        await _service.CreateAsync(value);
    }

    [HttpPut]
    public async Task Put([FromBody] Airline value)
    {
        await _service.UpdateAsync(value);
    }

    [HttpDelete("{id}")]
    public async Task Delete(int id)
    {
        await _service.DeleteAsync(id);
    }
}
