using Ardalis.Specification;
using Fly.Core.Entities;

namespace Fly.Core.Specifications;

public class AirlineSpec : Specification<Airline> , ISingleResultSpecification
{
    public AirlineSpec(int id)
    {
        Query.Include(x => x.Aircrafts);

        Query.Include(x => x.Managers).ThenInclude(x => x.User);

        Query.Where(x => x.Id == id);
    }
}
