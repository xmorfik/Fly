using Ardalis.Specification;
using Fly.Core.Entities;
using Fly.Core.Parameters;

namespace Fly.Core.Specifications;

public class FlightListSpec : Specification<Flight>
{
    public FlightListSpec(FlightParameter parameter)
    {
        Query.Include(x => x.Aircraft);

        Query.Include(x => x.DepartureAirport).ThenInclude(x => x.City);

        Query.Include(x => x.ArrivalAirport).ThenInclude(x => x.City);

		Query.Where(x => parameter.FlightState == null || x.FlightState == parameter.FlightState);

		Query.Where(x => parameter.DepartureDateTime == null || x.DepartureDateTime <= parameter.DepartureDateTime);

        Query.Where(x => parameter.ArrivalDateTime == null || x.ArrivalDateTime >= parameter.ArrivalDateTime);

        Query.Where(x => parameter.DepartureCity == null || x.DepartureAirport.City.Name.Contains(parameter.DepartureCity));

        Query.Where(x => parameter.ArrivalCity == null || x.ArrivalAirport.City.Name.Contains(parameter.ArrivalCity));

        Query.OrderByDescending(x => x.DepartureDateTime);
    }
}
