using Fly.WebAPI.Hubs;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;

namespace Fly.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LocationsController : ControllerBase
    {
        IHubContext<LocationHub> _hub;
        public LocationsController(IHubContext<LocationHub> hub)
        {
            _hub = hub;
        }

        [HttpPost]
        public async Task Get()
        {
            await _hub.Clients.All.SendAsync("Send", "Yeeeeeees");
            await _hub.Clients.All.SendAsync("Message", "Message");
        }
    }
}
