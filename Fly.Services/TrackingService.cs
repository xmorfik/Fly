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
    private readonly ITicketsStateService _ticketsStateService;
    private readonly IFlightStateService _flightStateService;
    public TrackingService(
        IAircraftLocationService<LocationDto> aircraftLocationService,
        IRouteBuilder<Flight, LocationDto> flightsRouteBuilder,
        IRepository<Flight> repository,
        IRepository<Aircraft> aircrafts,
        ITicketsStateService ticketsStateService,
        IFlightStateService flightStateService)
    {
        RecurringJob.AddOrUpdate(() => Update(), "*/10 * * * * *");
        _aircraftLocationService = aircraftLocationService;
        _flightsRouteBuilder = flightsRouteBuilder;
        _repository = repository;
        _aircrafts = aircrafts;
        _ticketsStateService = ticketsStateService;
        _flightStateService = flightStateService;
    }

    public async Task Track(int id)
    {
        var flight = await _repository.FirstOrDefaultAsync(new FlightSpec(id));
        await _flightStateService.Start(id);

        var location = _flightsRouteBuilder.GetLocation(flight);
        await _aircraftLocationService.CreateAsync(location);
    }

    public async Task Stop(int id)
    {
        var flight = await _repository.FirstOrDefaultAsync(new FlightSpec(id));
        await _flightStateService.End(id);

        await _aircraftLocationService.DeleteAsync(flight.Id ?? 0);
    }

    public async Task Update()
    {
        var flights = await _repository.ListAsync(
            new FlightListSpec(
                new FlightParameter { FlightState = FlightState.InProgress}));
        foreach (var flight in flights)
        {
            if(flight.ArrivalDateTime <= DateTime.Now)
            {
                await Stop(flight.Id ?? 0);
            }
            var location = _flightsRouteBuilder.GetLocation(flight);
            await _aircraftLocationService.UpdateAsync(location);
        }
    }
}
