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
public class AirlinesController : ControllerBase
{
    private readonly IService<Airline, AirlineParameter> _service;

    public AirlinesController(IService<Airline, AirlineParameter> service)
    {
        _service = service;
    }

    [HttpGet]
    [AllowAnonymous]
    public async Task<ICollection<Airline>> Get([FromQuery] AirlineParameter parameter, [FromQuery] Page page)
    {
        var result = await _service.GetListAsync(parameter, page);
        Response.Headers.Add("X-Pagination", JsonConvert.SerializeObject(result.MetaData));
        return result;
    }

    [HttpGet("{id}")]
    [AllowAnonymous]
    public async Task<Response<Airline>> Get(int id)
    {
        return await _service.GetAsync(id);
    }

    [HttpPost]
    [Authorize(Policy = "ManagerAndAdminOnly")]
    [ValidateModel]
    public async Task Post([FromBody] Airline value)
    {
        await _service.CreateAsync(value);
    }

    [HttpPut]
    [Authorize(Policy = "ManagerAndAdminOnly")]
    [ValidateModel]
    public async Task Put([FromBody] Airline value)
    {
        await _service.UpdateAsync(value);
    }

    [HttpDelete("{id}")]
    [Authorize(Policy = "ManagerAndAdminOnly")]
    public async Task Delete(int id)
    {
        await _service.DeleteAsync(id);
    }
}
