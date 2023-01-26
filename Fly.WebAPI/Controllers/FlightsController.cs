using AutoMapper;
using Fly.Core.DataTransferObjects;
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
        private readonly IMapper _mapper;

        public FlightsController(IService<Flight, FlightParameter> service, IMapper mapper)
        {
            _service = service;
            _mapper = mapper;
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
        public async Task Post([FromBody] FlightDTO value)
        {
            var item = _mapper.Map<Flight>(value);
            await _service.CreateAsync(item);
        }

        [HttpPut]
        public async Task Put([FromBody] FlightDTO value)
        {
            var item = _mapper.Map<Flight>(value);
            await _service.UpdateAsync(item);
        }

        [HttpDelete("{id}")]
        public async Task Delete(int id)
        {
            await _service.DeleteAsync(id);
        }
    }
}
