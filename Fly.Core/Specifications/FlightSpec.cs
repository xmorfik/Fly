using Ardalis.Specification;
using Fly.Core.Entities;

namespace Fly.Core.Specifications;

public class FlightSpec : Specification<Flight>, ISingleResultSpecification
{
    public FlightSpec(int id)
    {
        Query.Include(x => x.Aircraft);

        Query.Include(x => x.DepartureAirport.City);

        Query.Include(x => x.ArrivalAirport.City);

        Query.Include(x => x.Tickets);

        Query.Where(x => x.Id == id).AsNoTracking();
    }
}
