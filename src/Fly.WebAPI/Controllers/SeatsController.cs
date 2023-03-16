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
public class SeatsController : ControllerBase
{
    private readonly IService<Seat, SeatParameter> _service;

    public SeatsController(IService<Seat, SeatParameter> service)
    {
        _service = service;
    }

    [HttpGet]
    [AllowAnonymous]
    public async Task<ICollection<Seat>> Get([FromQuery] SeatParameter parameter, [FromQuery] Page page)
    {
        var result = await _service.GetListAsync(parameter, page);
        var metaData = JsonSerializer.Serialize(result.MetaData);
        Response.Headers.Add("X-Pagination", metaData);
        return result;
    }

    [HttpGet("{id}")]
    [AllowAnonymous]
    public async Task<ResponseBase<Seat>> Get(int id)
    {
        return await _service.GetAsync(id);
    }

    [HttpPost]
    [Authorize(Policy = "ManagerAndAdminOnly")]
    [ValidateModel]
    public async Task Post([FromBody] Seat value)
    {
        await _service.CreateAsync(value);
    }

    [HttpPut]
    [Authorize(Policy = "ManagerAndAdminOnly")]
    [ValidateModel]
    public async Task Put([FromBody] Seat value)
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
