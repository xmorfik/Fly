using Fly.Core.Entities;
using Fly.Core.Pagination;
using Fly.Core.Parameters;
using Fly.Core.Services;
using Fly.WebAPI.Filters;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace Fly.WebAPI.Controllers;

[Route("api/[controller]")]
[ApiController]
[Authorize(Policy = "ManagerAndAdminOnly")]
public class AircraftsController : ControllerBase
{
    private readonly IService<Aircraft, AircraftParameter> _service;

    public AircraftsController(IService<Aircraft, AircraftParameter> service)
    {
        _service = service;
    }

    [HttpGet]
    public async Task<ICollection<Aircraft>> Get([FromQuery] AircraftParameter parameter, [FromQuery] Page page)
    {
        var result = await _service.GetListAsync(parameter, page);
        Response.Headers.Add("X-Pagination", JsonConvert.SerializeObject(result.MetaData));
        return result;
    }

    [HttpGet("{id}")]
    public async Task<Response<Aircraft>> Get(int id)
    {
        return await _service.GetAsync(id);
    }

    [HttpPost]
    [ValidateModel]
    public async Task Post([FromBody] Aircraft value)
    {
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
