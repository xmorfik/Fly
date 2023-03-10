using Ardalis.Specification;
using Fly.Core.Entities;

namespace Fly.Core.Specifications;

public class AircraftSpec : Specification<Aircraft>, ISingleResultSpecification
{
    public AircraftSpec(int id)
    {
        Query.Include(x => x.Airline);

        Query.Include(x => x.Airport);

        Query.Include(x => x.Seats);

        Query.Where(x => x.Id == id);
    }
}
