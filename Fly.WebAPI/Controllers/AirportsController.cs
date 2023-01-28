using Fly.Core.Entities;
using Fly.Core.Pagination;
using Fly.Core.Parameters;
using Fly.Core.Services;
using Microsoft.AspNetCore.Mvc;

namespace Fly.WebAPI.Controllers;

[Route("api/[controller]")]
[ApiController]
public class AirportsController : ControllerBase
{
    private readonly IService<Airport, AirportParameter> _service;

    public AirportsController(IService<Airport, AirportParameter> service)
    {
        _service = service;
    }

    [HttpGet]
    public async Task<PagedResponse<ICollection<Airport>>> Get([FromQuery] AirportParameter parameter, [FromQuery] Page page)
    {
        return await _service.GetListAsync(parameter, page);
    }

    [HttpGet("{id}")]
    public async Task<Response<Airport>> Get(int id)
    {
        return await _service.GetAsync(id);
    }

    [HttpPost]
    public async Task Post([FromBody] Airport value)
    {
        await _service.CreateAsync(value);
    }

    [HttpPut]
    public async Task Put([FromBody] Airport value)
    {
        await _service.UpdateAsync(value);
    }

    [HttpDelete("{id}")]
    public async Task Delete(int id)
    {
        await _service.DeleteAsync(id);
    }
}
