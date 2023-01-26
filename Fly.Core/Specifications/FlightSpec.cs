using Ardalis.Specification;
using Fly.Core.Entities;

namespace Fly.Core.Specifications;

public class FlightSpec : Specification<Flight>, ISingleResultSpecification
{
    public FlightSpec(int id)
    {
        Query.Where(x => x.Id == id).AsNoTracking();
    }
}
