using Ardalis.Specification;
using AutoMapper;
using Fly.Core.DataTransferObjects;
using Fly.Core.Entities;

namespace Fly.Core.Specifications;

public class FlightListSpec : Specification<Flight, FlightDTO>
{
    public FlightListSpec(IMapper mapper)
    {
        Query.AsNoTracking();
        Query.Select(x => mapper.Map<FlightDTO>(x));
    }
}
