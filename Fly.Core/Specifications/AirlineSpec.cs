using Ardalis.Specification;
using Fly.Core.Entities;

namespace Fly.Core.Specifications;

public class AirlineSpec : Specification<Airline>
{
    public AirlineSpec(int id)
    {
        Query.Include(x => x.Aircrafts); 

        Query.Where(x => x.Id == id).AsNoTracking();
    }
}
