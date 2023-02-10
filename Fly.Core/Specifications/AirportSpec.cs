using Ardalis.Specification;
using Fly.Core.Entities;

namespace Fly.Core.Specifications;

public class AirportSpec : Specification<Airport>
{
    public AirportSpec(int id)
    {
        Query.Include(x => x.Aircrafts);

        Query.Include(x => x.City);

        Query.Include(x => x.FlightsIn);

        Query.Include(x => x.FlightsOut);

        Query.Where(x => x.Id == id).AsNoTracking();
    }
}
