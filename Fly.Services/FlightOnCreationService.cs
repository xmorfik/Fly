using Fly.Core.Entities;
using Fly.Core.Interfaces;
using Fly.Core.Services;
using Fly.Core.Specifications;

namespace Fly.Services;

public class FlightOnCreationService : IFlightOnCreationService
{
    private readonly IRepository<Flight> _repository;
    private readonly IRepository<Aircraft> _aircrafts;

    public FlightOnCreationService(
        IRepository<Flight> repository,
        IRepository<Aircraft> aircrafts)
    {
        _repository = repository;
        _aircrafts = aircrafts;
    }

    public async Task<bool> СheckFlight(Flight flight)
    {
        if(flight.DepartureDateTime >= flight.ArrivalDateTime)
        {
            return false;
        }

        var result = await _repository.FirstOrDefaultAsync(new NextFlightForAircraft(flight.AircraftId ?? 0));

        if (result == null)
        {
            return true;
        }

        if (result.ArrivalDateTime >= flight.DepartureDateTime)
        {
            return false;
        }

        return true;
    }

    public async Task<Flight> SetDepartureAirport(Flight flight)
    {
        var result = await _repository.FirstOrDefaultAsync(new NextFlightForAircraft(flight.AircraftId ?? 0));
        if (result == null)
        {
            var aircarft = await _aircrafts.FirstOrDefaultAsync(new AircraftSpec(flight.AircraftId ?? 0));
            flight.DepartureAirportId = aircarft.AirportId;
            return flight;
        }

        flight.DepartureAirportId = result.ArrivalAirportId;
        return flight;
    }
}
