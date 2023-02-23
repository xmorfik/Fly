using Ardalis.Specification;
using Fly.Core.Entities;
using Fly.Core.Parameters;

namespace Fly.Core.Specifications;

public class AirlineListSpec : Specification<Airline>
{
    public AirlineListSpec(AirlineParameter parameter)
    {
        Query.Where(x => parameter.Name == null || x.Name.Contains(parameter.Name));
    }
}
