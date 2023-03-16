using Fly.Core.Entities;
using Fly.Core.Pagination;
using Fly.Core.Parameters;
using Fly.Core.Services;
using Fly.WebAPI.Filters;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Text.Json;

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
    [AllowAnonymous]
    public async Task<ICollection<Airport>> Get([FromQuery] AirportParameter parameter, [FromQuery] Page page)
    {
        var result = await _service.GetListAsync(parameter, page);
        var metaData = JsonSerializer.Serialize(result.MetaData);
        Response.Headers.Add("X-Pagination", metaData);
        return result;
    }

    [HttpGet("{id}")]
    [AllowAnonymous]
    public async Task<ResponseBase<Airport>> Get(int id)
    {
        return await _service.GetAsync(id);
    }

    [HttpPost]
    [Authorize(Policy = "AdministratorOnly")]
    [ValidateModel]
    public async Task Post([FromBody] Airport value)
    {
        await _service.CreateAsync(value);
    }

    [HttpPut]
    [Authorize(Policy = "AdministratorOnly")]
    [ValidateModel]
    public async Task Put([FromBody] Airport value)
    {
        await _service.UpdateAsync(value);
    }

    [HttpDelete("{id}")]
    [Authorize(Policy = "AdministratorOnly")]
    public async Task Delete(int id)
    {
        await _service.DeleteAsync(id);
    }
}
