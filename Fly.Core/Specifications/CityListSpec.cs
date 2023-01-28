using Ardalis.Specification;
using Fly.Core.Entities;
using Fly.Core.Pagination;
using Fly.Core.Parameters;

namespace Fly.Core.Specifications;

public class CityListSpec : Specification<City>
{
    public CityListSpec(CityParameter parameter, Page page)
    {
        Query.Where(x => parameter == null || x.Name.Contains(parameter.Name));

        Query.Skip((page.PageNumber - 1) * page.PageSize)
            .Take(page.PageSize).OrderBy(x => x.Id);
    }
}
