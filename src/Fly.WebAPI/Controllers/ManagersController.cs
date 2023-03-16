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
[Authorize(Policy = "AdministratorOnly")]
public class ManagersController : ControllerBase
{
    private readonly IService<Manager, ManagerParameter> _service;
    public ManagersController(IService<Manager, ManagerParameter> service)
    {
        _service = service;
    }

    [HttpGet]
    [Authorize(Policy = "AdministratorOnly")]
    public async Task<ICollection<Manager>> Get([FromQuery] ManagerParameter parameter, [FromQuery] Page page)
    {
        var result = await _service.GetListAsync(parameter, page);
        var metaData = JsonSerializer.Serialize(result.MetaData);
        Response.Headers.Add("X-Pagination", metaData);
        return result;
    }

    [HttpGet("{id}")]
    [Authorize(Policy = "AdministratorOnly")]
    public async Task<ResponseBase<Manager>> Get(int id)
    {
        return await _service.GetAsync(id);
    }

    [HttpPost]
    [Authorize(Policy = "AdministratorOnly")]
    [ValidateModel]
    public async Task Post([FromBody] Manager value)
    {
        await _service.CreateAsync(value);
    }

    [HttpPut]
    [Authorize(Policy = "AdministratorOnly")]
    [ValidateModel]
    public async Task Put([FromBody] Manager value)
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
