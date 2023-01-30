using Ardalis.Specification;
using Fly.Core.Entities;
using Fly.Core.Pagination;
using Fly.Core.Parameters;

namespace Fly.Core.Specifications;

public class AircraftListSpec : Specification<Aircraft>
{
    public AircraftListSpec(AircraftParameter parameter, Page page)
    {
        Query.Where(x => parameter.AircraftState == null || x.AircraftState == parameter.AircraftState);

        Query.Where(x => parameter.Airline == null || x.Airline.Name.Contains(parameter.Airline));

        Query.Where(x => parameter.ModelType == null || x.ModelType.Contains(parameter.ModelType));

        Query.Skip((page.PageNumber - 1) * page.PageSize)
            .Take(page.PageSize).OrderBy(x => x.Id);
    }
}
