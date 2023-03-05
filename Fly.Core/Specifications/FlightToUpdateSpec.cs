using Ardalis.Specification;
using Fly.Core.Entities;

namespace Fly.Core.Specifications;

public class FlightToUpdateSpec : Specification<Flight>, ISingleResultSpecification
{
    public FlightToUpdateSpec(int id)
    {
        Query.Where(x => x.Id == id).AsNoTracking();
    }
}
