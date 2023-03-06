using Fly.Core.Entities;

namespace Fly.Core.Services;

public interface IFlightOnCreationService
{
    public Task<bool> СheckFlight(Flight flight);

    public Task<Flight> SetDepartureAirport(Flight flight);
}
