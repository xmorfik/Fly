using Fly.Shared.DataTransferObjects;
using Fly.WebAPI.Hubs;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;

namespace Fly.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LocationsController : ControllerBase
    {
        IHubContext<LocationHub> _hub;
        Random Random = new Random();
        public LocationsController(IHubContext<LocationHub> hub)
        {
            _hub = hub;
        }

        [HttpPost]
        public async Task Get()
        {
            var locationsTemp = new List<LocationDto>()
            {
                new LocationDto()
                {
                    Latitude=50 + Random.NextDouble(),
                    Longitude= 30 + Random.NextDouble(),
                    DirectionAngle= 60+Random.Next(360),
                    AircraftId= 0
                },
                new LocationDto()
                {
                    Latitude=60 + Random.NextDouble(),
                    Longitude= 30 + Random.NextDouble(),
                    DirectionAngle= 60+Random.Next(360),
                    AircraftId= 0
                },
                new LocationDto()
                {
                    Latitude=70 + Random.NextDouble(),
                    Longitude= 30 +Random.NextDouble(),
                    DirectionAngle= 60+Random.Next(360),
                    AircraftId= 0
                }
            };
            await _hub.Clients.All.SendAsync("Locations", locationsTemp);
        }
    }
}
