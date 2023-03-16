using Fly.Core.Entities;
using Fly.Core.Interfaces;
using Fly.Core.Services;
using Fly.Core.Specifications;
using GeoCoordinatePortable;

namespace Fly.Services;

public class FlightOnCreationService : IFlightOnCreationService
{
    private readonly IRepository<Flight> _repository;
    private readonly IRepository<Aircraft> _aircrafts;
    private readonly IRepository<Airport> _airports;
    private const int Speed = 900;

    public FlightOnCreationService(
        IRepository<Flight> repository,
        IRepository<Aircraft> aircrafts,
        IRepository<Airport> airports)
    {
        _repository = repository;
        _aircrafts = aircrafts;
        _airports = airports;
    }

    public async Task<bool> СheckFlight(Flight flight)
    {
        if (flight.DepartureDateTime >= flight.ArrivalDateTime)
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

    public async Task<Flight> SetArrivalDateTime(Flight flight)
    {
        if (flight.ArrivalDateTime != null)
        {
            return flight;
        }

        var departureAirport = await _airports.GetByIdAsync(flight.DepartureAirportId);
        var arrivalAirport = await _airports.GetByIdAsync(flight.ArrivalAirportId);
        var distance = CalculateDistanceKm(departureAirport, arrivalAirport);
        var flightTimeHours = (double)distance / Speed;
        var arrivalTime = flight.DepartureDateTime + TimeSpan.FromHours(flightTimeHours);

        flight.ArrivalDateTime = arrivalTime;
        return flight;
    }

    private int CalculateDistanceKm(Airport departureAirport, Airport arrivalAirport)
    {
        var sCoord = new GeoCoordinate(departureAirport.Latitude, departureAirport.Longitude);
        var eCoord = new GeoCoordinate(arrivalAirport.Latitude, arrivalAirport.Longitude);
        return (int)sCoord.GetDistanceTo(eCoord) / 1000;
    }
}
