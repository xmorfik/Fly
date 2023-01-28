using Ardalis.Specification;
using Fly.Core.Entities;

namespace Fly.Core.Specifications;

public class AircraftLocationSpec : Specification<AircraftLocation>
{
    public AircraftLocationSpec(int id)
    {
        Query.Where(x => x.Id == id).AsNoTracking();
    }
}
