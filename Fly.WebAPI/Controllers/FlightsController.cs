using Fly.Core.Entities;
using Fly.Core.Pagination;
using Fly.Core.Parameters;
using Fly.Core.Services;
using Microsoft.AspNetCore.Mvc;

namespace Fly.WebAPI.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class FlightsController : ControllerBase
    {
        private readonly IService<Flight, FlightParameter> _service;

        public FlightsController(IService<Flight, FlightParameter> service)
        {
            _service = service;
        }

        [HttpGet]
        public async Task<PagedResponse<ICollection<Flight>>> Get([FromQuery] FlightParameter parameter, [FromQuery] Page page)
        {
            return await _service.GetListAsync(parameter, page);
        }

        [HttpGet("{id}")]
        public async Task<Response<Flight>> Get(int id)
        {
            return await _service.GetAsync(id);
        }

        [HttpPost]
        public async Task Post([FromBody] Flight value)
        {
            await _service.CreateAsync(value);
        }

        [HttpPut]
        public async Task Put([FromBody] Flight value)
        {
            await _service.UpdateAsync(value);
        }

        [HttpDelete("{id}")]
        public async Task Delete(int id)
        {
            await _service.DeleteAsync(id);
        }
    }
}
