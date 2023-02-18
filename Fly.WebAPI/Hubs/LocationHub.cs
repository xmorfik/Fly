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

    public async Task Send(string message)
    {
        await Clients.All.SendAsync("Send",message);
    }

    public async Task Message(string message)
    {
        await Clients.All.SendAsync("Message", message);
    }
}
