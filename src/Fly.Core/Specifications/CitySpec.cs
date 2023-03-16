using Ardalis.Specification;
using Fly.Core.Entities;

namespace Fly.Core.Specifications;

public class CitySpec : Specification<City>, ISingleResultSpecification
{
    public CitySpec(int id)
    {
        Query.Where(x => x.Id == id);
    }
}
