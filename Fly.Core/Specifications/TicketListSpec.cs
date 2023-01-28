using Ardalis.Specification;
using Fly.Core.Entities;
using Fly.Core.Pagination;
using Fly.Core.Parameters;

namespace Fly.Core.Specifications;

public class TicketListSpec : Specification<Ticket>
{
    public TicketListSpec(TicketParameter parameter, Page page)
    {
        Query.Where(x => parameter.TicketState == null || x.TicketState == parameter.TicketState);

        Query.Where(x => parameter.PriceMin == null || x.Price >= parameter.PriceMin);

        Query.Where(x => parameter.PriceMax == null || x.Price <= parameter.PriceMax);

        Query.Where(x => parameter.DepartureCity == null || x.Flight.DepartureAirport.City.Name.Contains(parameter.DepartureCity));

        Query.Where(x => parameter.ArrivalCity == null || x.Flight.ArrivalAirport.City.Name.Contains(parameter.ArrivalCity));

        Query.Skip((page.PageNumber - 1) * page.PageSize)
            .Take(page.PageSize).OrderBy(x => x.Id);
    }
}
