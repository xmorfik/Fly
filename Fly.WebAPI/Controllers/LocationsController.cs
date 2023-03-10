using Fly.Core.Services;
using Fly.Shared.DataTransferObjects;
using Fly.WebAPI.Hubs;
using Hangfire;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;

namespace Fly.WebAPI.Controllers;

[Route("api/[controller]")]
[ApiController]
public class LocationsController : ControllerBase
{
    private readonly IHubContext<LocationHub> _hub;
    private readonly IAircraftLocationService<LocationDto> _aircraftLocationService;

    public LocationsController(
        IHubContext<LocationHub> hub,
        IAircraftLocationService<LocationDto> aircraftLocationService)
    {
        RecurringJob.AddOrUpdate(() => Get(), "*/10 * * * * *");
        _aircraftLocationService = aircraftLocationService;
        _hub = hub;
    }


    [HttpGet]
    public async Task Get()
    {
        var locations = await _aircraftLocationService.GetСurrentLocations();
        await _hub.Clients.All.SendAsync("Locations", locations);
    }

    [HttpPost]
    public async Task Post(int id)
    {
        await _hub.Clients.All.SendAsync("LocationsHistory", new LocationDto());
    }
}
