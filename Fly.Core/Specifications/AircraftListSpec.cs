using Ardalis.Specification;
using Fly.Core.Entities;
using Fly.Core.Pagination;
using Fly.Core.Parameters;

namespace Fly.Core.Specifications;

public class AircraftListSpec : Specification<Aircraft>
{
    public AircraftListSpec(AircraftParameter parameter, Page page)
    {
        Query.Skip((page.PageNumber - 1) * page.PageSize)
            .Take(page.PageSize);
    }
}
