using Fly.Core.Entities;
using Fly.Core.Enums;
using Fly.Core.Interfaces;
using Fly.Core.Services;

namespace Fly.Services;

public class FlightStateService : IFlightStateService
{
    private readonly ITicketsStateService _ticketsStateService;
    private readonly IAircraftStateService _aircraftStateService;
    private readonly IRepository<Flight> _repository;
    public FlightStateService(
        IAircraftStateService aircraftStateService,
        IRepository<Flight> repository,
        ITicketsStateService ticketsStateService)
    {
        _aircraftStateService = aircraftStateService;
        _repository = repository;
        _ticketsStateService = ticketsStateService;
    }

    public async Task End(int id)
    {
        var flight = await _repository.GetByIdAsync(id);
        flight.FlightState = FlightState.Completed;
        await _aircraftStateService.Landing(flight.AircraftId ?? 0, flight.ArrivalAirportId ?? 0);
        await _repository.UpdateAsync(flight);
    }

    public async Task Start(int id)
    {
        await _ticketsStateService.SetTicketsStateOnStartFlight(id);
        var flight = await _repository.GetByIdAsync(id);
        await _aircraftStateService.Takeoff(flight.AircraftId ?? 0);
        flight.FlightState = FlightState.InProgress;
        await _repository.UpdateAsync(flight);
    }
}
