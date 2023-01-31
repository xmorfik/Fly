using Ardalis.Specification;
using Fly.Core.Entities;

namespace Fly.Core.Specifications;

public class ManagerSpec : Specification<Manager>, ISingleResultSpecification
{
    public ManagerSpec(int id)
    {
        Query.Where(x => x.Id == id).AsNoTracking();
    }
}
