using Fly.Core.Entities;
using Fly.Core.Interfaces;
using Fly.Core.Services;
using Fly.Core.Specifications;
using Fly.Shared.DataTransferObjects;
using Microsoft.AspNetCore.SignalR;

namespace Fly.WebAPI.Hubs;

public class LocationHub : Hub
{
    private readonly IAircraftLocationService<LocationDto> _aircraftLocationService;
    private readonly IRepository<Flight> _repositoryFlights;
    public LocationHub(
        IAircraftLocationService<LocationDto> aircraftLocationService,
        IRepository<Flight> repositoryFlights)
    {
        _aircraftLocationService = aircraftLocationService;
        _repositoryFlights = repositoryFlights;
    }

    public async Task SendLocationsAsync()
    {
        var locations = await _aircraftLocationService.GetСurrentLocations();
        await Clients.All.SendAsync("Locations", locations);
    }

    //public async Task SendLocationsHistoryAsync(int id)
    //{
    //    var locations = await _aircraftLocationService.GetLocations(id);
    //    await Clients.All.SendAsync("SendLocationsHistoryAsync", locations);
    //}

    public async Task GetFlight(int id)
    {
        var flight = await _repositoryFlights.FirstOrDefaultAsync(new FlightSpec(DateTime.Now, id));
        var flightDto = new FlightDto
        {
            FlightId = flight.Id ??= 0,
            DepartureLatitude = flight.DepartureAirport.Latitude,
            DepartureLongitude = flight.DepartureAirport.Longitude,
            ArrivalLatitude = flight.ArrivalAirport.Latitude,
            ArrivalLongitude = flight.ArrivalAirport.Longitude,
        };
        await Clients.Caller.SendAsync("Flight", flightDto);
    }
}
