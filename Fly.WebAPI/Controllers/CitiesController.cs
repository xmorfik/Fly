﻿using Fly.Core.Entities;
using Fly.Core.Pagination;
using Fly.Core.Parameters;
using Fly.Core.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

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
    public async Task<PagedResponse<ICollection<City>>> Get([FromQuery] CityParameter parameter, [FromQuery] Page page)
    {
        return await _service.GetListAsync(parameter, page);
    }

    [HttpGet("{id}")]
    [AllowAnonymous]
    public async Task<Response<City>> Get(int id)
    {
        return await _service.GetAsync(id);
    }

    [HttpPost]
    [Authorize(Roles = "Manager")]
    public async Task Post([FromBody] City value)
    {
        await _service.CreateAsync(value);
    }

    [HttpPut]
    [Authorize(Roles = "Manager")]
    public async Task Put([FromBody] City value)
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
