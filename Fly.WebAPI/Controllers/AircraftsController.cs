using Fly.Core;
using Fly.Core.Entities;
using Fly.Core.Pagination;
using Fly.Core.Parameters;
using Fly.Core.Services;
using Fly.WebAPI.Filters;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using System.Text.Json;

namespace Fly.WebAPI.Controllers;

[Route("api/[controller]")]
[ApiController]
[Authorize(Policy = "ManagerAndAdminOnly")]
public class AircraftsController : ControllerBase
{
    private readonly IService<Aircraft, AircraftParameter> _service;
    private readonly ILogger<AircraftsController> _logger;

    public AircraftsController(IService<Aircraft, AircraftParameter> service,
        ILogger<AircraftsController> logger)
    {
        _service = service;
        _logger = logger;
    }

    [HttpGet]
    public async Task<ICollection<Aircraft>> Get([FromQuery] AircraftParameter parameter, [FromQuery] Page page)
    {
        try
        {
            var airlineId = User.FindFirstValue(Claims.Airline);

            if (airlineId != null)
            {
                parameter.AirlineId = int.Parse(airlineId);
            }
        }
        catch (Exception ex)
        {
            _logger.LogError(ex.Message);
        }

        var result = await _service.GetListAsync(parameter, page);
        var metaData = JsonSerializer.Serialize(result.MetaData);
        Response.Headers.Add("X-Pagination", metaData);
        return result;
    }

    [HttpGet("{id}")]
    public async Task<ResponseBase<Aircraft>> Get(int id)
    {
        var result = await _service.GetAsync(id);
        return result;
    }

    [HttpPost]
    [ValidateModel]
    public async Task Post([FromBody] Aircraft value)
    {
        try
        {
            var airlineId = User.FindFirstValue(Claims.Airline);

            if (airlineId != null)
            {
                value.AirlineId = int.Parse(airlineId);
            }
        }
        catch (Exception ex)
        {
            _logger.LogError(ex.Message);
        }
        await _service.CreateAsync(value);
    }

    [HttpPut]
    [ValidateModel]
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
