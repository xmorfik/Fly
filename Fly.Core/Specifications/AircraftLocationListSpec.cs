using Ardalis.Specification;
using Fly.Core.Entities;
using Fly.Core.Pagination;
using Fly.Core.Parameters;

namespace Fly.Core.Specifications;

public class AircraftLocationListSpec : Specification<AircraftLocation>
{
    public AircraftLocationListSpec(AircraftLocationParameter parameter, Page page)
    {
        Query.Where(x => parameter.ModelType == null || x.Aircraft.ModelType.Contains(parameter.ModelType));

        Query.Where(x => parameter.SerialNumber == null || x.Aircraft.SerialNumber.Contains(parameter.SerialNumber));

        Query.Skip((page.PageNumber - 1) * page.PageSize)
            .Take(page.PageSize).OrderBy(x => x.Id);
    }
}
