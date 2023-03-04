using Ardalis.Specification;
using Fly.Core.Entities;
using Fly.Core.Pagination;
using Fly.Core.Parameters;
using System.Linq.Expressions;
using System.Reflection;

namespace Fly.Core.Specifications;

public class AircraftListSpec : Specification<Aircraft>
{
    public AircraftListSpec(AircraftParameter parameter, Page? page)
    {
        Query.Include(x => x.Airline);

        Query.Include(x => x.Airport).ThenInclude(x => x.City);

        Query.Where(x => parameter.AircraftState == null || x.AircraftState == parameter.AircraftState);

        Query.Where(x => parameter.Airline == null || x.Airline.Name.Contains(parameter.Airline));

        Query.Where(x => parameter.ModelType == null || x.ModelType.Contains(parameter.ModelType));

        Query.Where(x => parameter.AirlineId == null || x.Airline.Id == parameter.AirlineId);

        if (page != null)
        {
            Query.Skip((page.PageNumber - 1) * page.PageSize).Take(page.PageSize).OrderByDescending(x => x.Id);
        }
    }
}
