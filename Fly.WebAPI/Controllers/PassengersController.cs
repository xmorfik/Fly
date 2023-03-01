using Fly.Core.Entities;
using Fly.Core.Pagination;
using Fly.Core.Parameters;
using Fly.Core.Services;
using Fly.WebAPI.Filters;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace Fly.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PassengersController : ControllerBase
    {
        private readonly IService<Passenger, PassengerParameter> _service;

        public PassengersController(IService<Passenger, PassengerParameter> service)
        {
            _service = service;
        }

        [HttpGet]
        [AllowAnonymous]
        public async Task<ICollection<Passenger>> Get([FromQuery] PassengerParameter parameter, [FromQuery] Page page)
        {
            var result = await _service.GetListAsync(parameter, page);
            Response.Headers.Add("X-Pagination", JsonConvert.SerializeObject(result.MetaData));
            return result;
        }

        [HttpGet("{id}")]
        [AllowAnonymous]
        public async Task<ResponseBase<Passenger>> Get(int id)
        {
            return await _service.GetAsync(id);
        }

        [HttpPost]
        [Authorize(Policy = "AdministratorOnly")]
        [ValidateModel]
        public async Task Post([FromBody] Passenger value)
        {
            await _service.CreateAsync(value);
        }

        [HttpPut]
        [Authorize(Policy = "AdministratorOnly")]
        [ValidateModel]
        public async Task Put([FromBody] Passenger value)
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
}
