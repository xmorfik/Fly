using Fly.Core.Entities;
using Fly.Core.Enums;
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
    private readonly IRepository<Aircraft> _aircrafts;
    private readonly IAircraftLocationService<LocationDto> _aircraftLocationService;
    private readonly IRouteBuilder<Flight, LocationDto> _flightsRouteBuilder;
    public TrackingService(
        IAircraftLocationService<LocationDto> aircraftLocationService,
        IRouteBuilder<Flight, LocationDto> flightsRouteBuilder,
        IRepository<Flight> repository, 
        IRepository<Aircraft> aircrafts)
    {
        RecurringJob.AddOrUpdate(() => Update(), "*/10 * * * * *");
        _aircraftLocationService = aircraftLocationService;
        _flightsRouteBuilder = flightsRouteBuilder;
        _repository = repository;
        _aircrafts = aircrafts;
    }

    public async Task Track(int id)
    {
        var flight = await _repository.FirstOrDefaultAsync(new FlightSpec(id));
        var aircarft = await _aircrafts.FirstOrDefaultAsync(new AircraftSpec(flight.AircraftId ?? 0));
        aircarft.AircraftState = AircraftState.InAir;
        await _aircrafts.UpdateAsync(aircarft);
        if (flight == null)
        {
            return;
        }
        var location = _flightsRouteBuilder.GetLocation(flight);
        await _aircraftLocationService.CreateAsync(location);
    }

    public async Task Stop(int id)
    {
        var flight = await _repository.FirstOrDefaultAsync(new FlightSpec(id));
        var aircarft = await _aircrafts.FirstOrDefaultAsync(new AircraftSpec(flight.AircraftId ?? 0));
        aircarft.AircraftState = AircraftState.InAirport;
        aircarft.AirportId = flight.ArrivalAirportId;
        aircarft.Airport = null;
        await _aircrafts.UpdateAsync(aircarft);
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
