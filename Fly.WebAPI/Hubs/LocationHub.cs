using Fly.Core.Services;
using Fly.Shared.DataTransferObjects;
using Microsoft.AspNetCore.SignalR;

namespace Fly.WebAPI.Hubs;

public class LocationHub : Hub
{
    private readonly IAircraftLocationService<LocationDto> _aircraftLocationService;
    public LocationHub(IAircraftLocationService<LocationDto> aircraftLocationService)
    {
        _aircraftLocationService = aircraftLocationService;
    }

    public async Task SendLocationsAsync()
    {
        var locations = await _aircraftLocationService.GetСurrentLocations();
        await Clients.All.SendAsync("Locations", locations);
    }
    public async Task SendLocationsHistoryAsync(int id)
    {
        //var locations = await _aircraftLocationService.GetLocations(id);
        await Clients.All.SendAsync("SendLocationsHistoryAsync", new LocationDto());
    }
}
