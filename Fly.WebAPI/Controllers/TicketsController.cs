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
public class TicketsController : ControllerBase
{
    private readonly IService<Ticket, TicketParameter> _service;

    public TicketsController(IService<Ticket, TicketParameter> service)
    {
        _service = service;
    }

    [HttpGet]
    [AllowAnonymous]
    public async Task<ICollection<Ticket>> Get([FromQuery] TicketParameter parameter, [FromQuery] Page page)
    {
        var result = await _service.GetListAsync(parameter, page);
        Response.Headers.Add("X-Pagination", JsonConvert.SerializeObject(result.MetaData));
        return result;
    }

    [HttpGet("{id}")]
    [AllowAnonymous]
    public async Task<ResponseBase<Ticket>> Get(int id)
    {
        return await _service.GetAsync(id);
    }

    [HttpPost]
    [Authorize(Policy = "ManagerAndAdminOnly")]
    [ValidateModel]
    public async Task Post([FromBody] Ticket value)
    {
        await _service.CreateAsync(value);
    }

    [HttpPut]
    [Authorize]
    [ValidateModel]
    public async Task Put([FromBody] Ticket value)
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
