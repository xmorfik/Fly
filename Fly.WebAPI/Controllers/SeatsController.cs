using Fly.Core.Entities;
using Fly.Core.Pagination;
using Fly.Core.Parameters;
using Fly.Core.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

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
    public async Task<PagedResponse<ICollection<Seat>>> Get([FromQuery] SeatParameter parameter, [FromQuery] Page page)
    {
        return await _service.GetListAsync(parameter, page);
    }

    [HttpGet("{id}")]
    public async Task<Response<Seat>> Get(int id)
    {
        return await _service.GetAsync(id);
    }

    [HttpPost]
    [Authorize(Roles = "Manager")]
    public async Task Post([FromBody] Seat value)
    {
        await _service.CreateAsync(value);
    }

    [HttpPut]
    [Authorize(Roles = "Manager")]
    public async Task Put([FromBody] Seat value)
    {
        await _service.UpdateAsync(value);
    }

    [HttpDelete("{id}")]
    [Authorize(Roles = "Manager")]
    public async Task Delete(int id)
    {
        await _service.DeleteAsync(id);
    }
}
