using Ardalis.Specification;
using Fly.Core.Entities;

namespace Fly.Core.Specifications;

public class AirportSpec : Specification<Airport>
{
    public AirportSpec(int id)
    {
        Query.Where(x => x.Id == id).AsNoTracking();
    }
}
