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

        Query.Include(x => x.Tickets).ThenInclude(x => x.Seat);

        Query.Where(x => x.Id == id);
    }
}
