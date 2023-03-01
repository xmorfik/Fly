using Ardalis.Specification;
using Fly.Core.Entities;
using Fly.Core.Parameters;

namespace Fly.Core.Specifications;

public class ManagerListSpec : Specification<Manager>
{
    public ManagerListSpec(ManagerParameter parameter)
    {
        Query.Where(x => parameter.AirlineName == null || x.Airline.Name.Contains(parameter.AirlineName));

        Query.Where(x => parameter.UserName == null || x.User.UserName.Contains(parameter.UserName));

        Query.Include(x => x.User);

        Query.Include(x => x.Airline);
    }
}
