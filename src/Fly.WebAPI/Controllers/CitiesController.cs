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
public class CitiesController : ControllerBase
{
    private readonly IService<City, CityParameter> _service;

    public CitiesController(IService<City, CityParameter> service)
    {
        _service = service;
    }

    [HttpGet]
    [AllowAnonymous]
    public async Task<ICollection<City>> Get([FromQuery] CityParameter parameter, [FromQuery] Page page)
    {
        var result = await _service.GetListAsync(parameter, page);
        var metaData = JsonSerializer.Serialize(result.MetaData);
        Response.Headers.Add("X-Pagination", metaData);
        return result;
    }

    [HttpGet("{id}")]
    [AllowAnonymous]
    public async Task<ResponseBase<City>> Get(int id)
    {
        return await _service.GetAsync(id);
    }

    [HttpPost]
    [Authorize(Policy = "ManagerAndAdminOnly")]
    [ValidateModel]
    public async Task Post([FromBody] City value)
    {
        await _service.CreateAsync(value);
    }

    [HttpPut]
    [Authorize(Policy = "ManagerAndAdminOnly")]
    [ValidateModel]
    public async Task Put([FromBody] City value)
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
