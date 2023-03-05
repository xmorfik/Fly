using Ardalis.Specification;
using Fly.Core.Entities;

namespace Fly.Core.Specifications;

public class AirportSpec : Specification<Airport>, ISingleResultSpecification
{
    public AirportSpec(int id)
    {
        Query.Include(x => x.Aircrafts);

        Query.Include(x => x.City);

        Query.Where(x => x.Id == id);
    }
}
