using Ardalis.Specification;
using Fly.Core.Entities;
using Fly.Core.Pagination;
using Fly.Core.Parameters;

namespace Fly.Core.Specifications;

public class FlightListSpec : Specification<Flight>
{
    public FlightListSpec(FlightParameter parameter, Page page)
    {
        Query.Include(x => x.Aircraft);

        Query.Include(x => x.DepartureAirport.City);

        Query.Include(x => x.ArrivalAirport.City);

        Query.Include(x => x.Tickets);

        Query.Where(x => parameter.DepartureDateTime == null || x.DepartureDateTime >= parameter.DepartureDateTime);

        Query.Where(x => parameter.DepartureCity == null || x.DepartureAirport.City.Name.Contains(parameter.DepartureCity));

        Query.Where(x => parameter.ArrivalCity == null || x.ArrivalAirport.City.Name.Contains(parameter.ArrivalCity));

        Query.Skip((page.PageNumber - 1) * page.PageSize)
            .Take(page.PageSize).OrderBy(x => x.Id);
    }
}
