using Ardalis.Specification;
using AutoMapper;
using Fly.Core.DataTransferObjects;
using Fly.Core.Entities;

namespace Fly.Core.Specifications;

public class FlightSpec : Specification<Flight,FlightDTO>, ISingleResultSpecification
{
    public FlightSpec(int id, IMapper mapper)
    {
        Query.Where(x => x.Id == id).AsNoTracking();
        Query.Select(x => mapper.Map<FlightDTO>(x));
    }
}
