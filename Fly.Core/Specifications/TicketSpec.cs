using Ardalis.Specification;
using Fly.Core.Entities;

namespace Fly.Core.Specifications;

public class TicketSpec : Specification<Ticket>, ISingleResultSpecification
{
    public TicketSpec(int id)
    {
        Query.Where(x => x.Id == id);

        Query.Include(x => x.Seat);

        Query.Include(x => x.Passenger);

        Query.Include(x => x.Flight);

        Query.Include(x => x.Flight.ArrivalAirport);

        Query.Include(x => x.Flight.DepartureAirport);
    }
}
