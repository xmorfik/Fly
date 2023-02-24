using Ardalis.Specification;
using Fly.Core.Entities;

namespace Fly.Core.Specifications;

public class FlightSpec : Specification<Flight>, ISingleResultSpecification
{
    public FlightSpec(int id)
    {
        Query.Include(x => x.Aircraft).ThenInclude(x => x.Seats);

        Query.Include(x => x.DepartureAirport).ThenInclude(x => x.City);

        Query.Include(x => x.ArrivalAirport).ThenInclude(x => x.City);

        Query.Where(x => x.Id == id);
    }

    public FlightSpec(DateTime date, int aircarfatId)
    {
        Query.Include(x => x.Aircraft);

        Query.Include(x => x.DepartureAirport);

        Query.Include(x => x.ArrivalAirport);

        Query.Where(x => x.AircraftId == aircarfatId).OrderByDescending(x => x.DepartureDateTime);

        Query.Where(x => x.DepartureDateTime <= date);

        Query.Where(x => x.ArrivalDateTime >= date);
    }
}
