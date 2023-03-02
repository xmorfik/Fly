using Ardalis.Specification;
using Fly.Core.Entities;

namespace Fly.Core.Specifications;

public class TicketSpec : Specification<Ticket>
{
    public TicketSpec(int id)
    {
        Query.Where(x => x.Id == id);

        Query.Include(x => x.Seat);

        Query.Include(x => x.Passenger);

        Query.Include(x => x.Flight).ThenInclude(x => x.ArrivalAirport);

        Query.Include(x => x.Flight).ThenInclude(x => x.DepartureAirport);
    }
}
