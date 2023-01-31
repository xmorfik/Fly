using Ardalis.Specification;
using Fly.Core.Entities;
using Fly.Core.Pagination;
using Fly.Core.Parameters;

namespace Fly.Core.Specifications;

public class ManagerListSpec : Specification<Manager>
{
    public ManagerListSpec(ManagerParameter parameter, Page page)
    {
        Query.Where(x => parameter.AirlineName == null || x.Airline.Name.Contains(parameter.AirlineName));

        Query.Where(x => parameter.UserName == null || x.User.UserName.Contains(parameter.UserName));

        Query.Skip((page.PageNumber - 1) * page.PageSize)
            .Take(page.PageSize).OrderBy(x => x.Id);
    }
}
