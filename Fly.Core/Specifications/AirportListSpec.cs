using Ardalis.Specification;
using Fly.Core.Entities;
using Fly.Core.Pagination;
using Fly.Core.Parameters;

namespace Fly.Core.Specifications;

public class AirportListSpec : Specification<Airport>
{
    public AirportListSpec(AirportParameter parameter, Page? page)
    {
        Query.Include(x => x.City);

        Query.Where(x => parameter.CityName == null || x.City.Name.Contains(parameter.CityName));

        Query.Where(x => parameter.IsoRegion == null || x.City.IsoRegion.Contains(parameter.IsoRegion));

        Query.Where(x => parameter.IsoCountry == null || x.City.IsoCountry.Contains(parameter.IsoCountry));

        Query.Where(x => parameter.Name == null || x.Name.Contains(parameter.Name));

        if(page != null)
        {
            Query.Skip((page.PageNumber - 1) * page.PageSize).Take(page.PageSize).OrderByDescending(x => x.Id);
        }
    }
}
