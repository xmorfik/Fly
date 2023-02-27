using Fly.Core.Entities;
using Fly.Core.Pagination;
using Fly.Core.Parameters;
using Fly.Core.Services;
using Fly.WebAPI.Filters;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Security.Claims;

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
            var claims = ClaimsPrincipal.Current.Identities.First().Claims.ToList();
            var airlineId = claims?.FirstOrDefault(x => x.Type.Equals("Airline", StringComparison.OrdinalIgnoreCase))?.Value;
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
        Response.Headers.Add("X-Pagination", JsonConvert.SerializeObject(result.MetaData));
        return result;
    }

    [HttpGet("{id}")]
    public async Task<Response<Aircraft>> Get(int id)
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
            var claims = ClaimsPrincipal.Current.Identities.First().Claims.ToList();
            var airlineId = claims?.FirstOrDefault(x => x.Type.Equals("Airline", StringComparison.OrdinalIgnoreCase))?.Value;
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
