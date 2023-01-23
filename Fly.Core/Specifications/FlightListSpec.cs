using Ardalis.Specification;
using AutoMapper;
using Fly.Core.DataTransferObjects;
using Fly.Core.Entities;
using Fly.Core.Parameters;

namespace Fly.Core.Specifications;

public class FlightListSpec : Specification<Flight, FlightDTO>
{
    public FlightListSpec(IMapper mapper)
    {
        Query.AsNoTracking();
        Query.Select(x => mapper.Map<FlightDTO>(x));
    }

    public FlightListSpec(IMapper mapper, FlightParameter parameter)
    {
        Query.Include(x => x.ArrivalAirport)
            .Include(x => x.DepartureAirport);

        Query.Where(x => parameter.DepartureDateTime == null ||
            x.DepartureDateTime >= parameter.DepartureDateTime);

        Query.Where(x => parameter.DepartureCity == null ||
            x.DepartureAirport.City == null ||
            x.DepartureAirport.City.Contains(parameter.DepartureCity));

        Query.Where(x => parameter.ArrivalCity == null ||
            x.ArrivalAirport.City == null ||
            x.ArrivalAirport.City.Contains(parameter.ArrivalCity));

        Query.Select(x => mapper.Map<FlightDTO>(x));
    }
}
