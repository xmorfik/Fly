using Fly.Core.Entities;
using Fly.Core.Enums;
using Fly.Core.Interfaces;
using Fly.Core.Services;
using Fly.Core.Specifications;

namespace Fly.Services;

public class AircraftStateService : IAircraftStateService
{
    private readonly IRepository<Aircraft> _repository;
    public AircraftStateService(IRepository<Aircraft> repository)
    {
        _repository = repository;
    }

    public async Task Landing(int id, int airportId)
    {
        var aircarft = await _repository.GetByIdAsync(id);
        aircarft.AircraftState = AircraftState.InAirport;
        aircarft.AirportId = airportId;
        await _repository.UpdateAsync(aircarft);
    }

    public async Task Takeoff(int id)
    {
        var aircarft = await _repository.GetByIdAsync(id);
        aircarft.AircraftState = AircraftState.InAir;
        await _repository.UpdateAsync(aircarft);
    }
}
