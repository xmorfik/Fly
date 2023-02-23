using Ardalis.Specification;
using Fly.Core.Entities;
using Fly.Core.Parameters;

namespace Fly.Core.Specifications;

public class CityListSpec : Specification<City>
{
    public CityListSpec(CityParameter parameter)
    {
        Query.Where(x => parameter.Name == null || x.Name.Contains(parameter.Name));
    }
}
