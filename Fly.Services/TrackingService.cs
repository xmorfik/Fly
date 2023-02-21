using Fly.Core.Entities;
using Fly.Core.Interfaces;
using Fly.Core.Parameters;
using Fly.Core.Services;
using Fly.Core.Specifications;
using Fly.Shared.DataTransferObjects;
using Hangfire;

namespace Fly.Services;

public class TrackingService : ITrackingService
{
    private readonly IRepository<Flight> _repository;
    private readonly IAircraftLocationService<LocationDto> _aircraftLocationService;
    private readonly IRouteBuilder<Flight, LocationDto> _flightsRouteBuilder;
    public TrackingService(
        IAircraftLocationService<LocationDto> aircraftLocationService,
        IRouteBuilder<Flight, LocationDto> flightsRouteBuilder,
        IRepository<Flight> repository)
    {
        RecurringJob.AddOrUpdate(() => Update(), "*/10 * * * * *");
        _aircraftLocationService = aircraftLocationService;
        _flightsRouteBuilder = flightsRouteBuilder;
        _repository = repository;
    }

    public async Task Track(int id)
    {
        var flight = await _repository.FirstOrDefaultAsync(new FlightSpec(id));
        if (flight == null)
        {
            return;
        }
        var location = new LocationDto
        {
            AircraftId = flight.AircraftId,
            Longitude = flight.DepartureAirport.Longitude,
            Latitude = flight.DepartureAirport.Latitude,
            DirectionAngle = 90
        };
        await _aircraftLocationService.CreateAsync(location);
    }

    public async Task Stop(int id)
    {
        var flight = await _repository.FirstOrDefaultAsync(new FlightSpec(id));
        if (flight == null)
        {
            return;
        }
        await _aircraftLocationService.DeleteAsync(flight.AircraftId ?? 0);
    }

    public async Task Update()
    {
        var flights = await _repository.ListAsync(new FlightListSpec(new FlightParameter { ArrivalDateTime = DateTime.Now }));
        foreach (var flight in flights)
        {
            var location = _flightsRouteBuilder.GetLocation(flight);
            await _aircraftLocationService.UpdateAsync(location);
        }
    }
}
