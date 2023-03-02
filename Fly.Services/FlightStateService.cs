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
        var flightToUpdate = await _repository.GetByIdAsync(id);
        flightToUpdate.FlightState = FlightState.Completed;
        await _aircraftStateService.Landing(flightToUpdate.AircraftId ?? 0, flightToUpdate.ArrivalAirportId ?? 0);
        await _repository.UpdateAsync(flightToUpdate);
    }

    public async Task Start(int id)
    {
        var flightToUpdate = await _repository.GetByIdAsync(id);
        flightToUpdate.FlightState = FlightState.InProgress;
        await _ticketsStateService.SetTicketsStateOnStartFlight(id);
        await _aircraftStateService.Takeoff(flightToUpdate.AircraftId ?? 0);
        await _repository.UpdateAsync(flightToUpdate);
    }
}
