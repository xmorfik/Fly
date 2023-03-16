using Ardalis.Specification;
using Fly.Core.Entities;

namespace Fly.Core.Specifications;

public class SeatSpec : Specification<Seat>, ISingleResultSpecification
{
    public SeatSpec(int id)
    {
        Query.Where(x => x.Id == id);
    }
}
