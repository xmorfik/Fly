using Fly.Core.Entities;
using Fly.Core.Pagination;
using Fly.Core.Parameters;
using Fly.Core.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Fly.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ManagersController : ControllerBase
    {
        private readonly IService<Manager, ManagerParameter> _service;
        public ManagersController(IService<Manager, ManagerParameter> service)
        {
            _service = service;
        }

        [HttpGet]
        public async Task<PagedResponse<ICollection<Manager>>> Get([FromQuery] ManagerParameter parameter, [FromQuery] Page page)
        {
            return await _service.GetListAsync(parameter, page);
        }

        [HttpGet("{id}")]
        public async Task<Response<Manager>> Get(int id)
        {
            return await _service.GetAsync(id);
        }

        [HttpPost]
        [Authorize(Roles = "Administrator")]
        public async Task Post([FromBody] Manager value)
        {
            await _service.CreateAsync(value);
        }

        [HttpPut]
        [Authorize(Roles = "Administrator")]
        public async Task Put([FromBody] Manager value)
        {
            await _service.UpdateAsync(value);
        }

        [HttpDelete("{id}")]
        [Authorize(Roles = "Administrator")]
        public async Task Delete(int id)
        {
            await _service.DeleteAsync(id);
        }
    }
}
