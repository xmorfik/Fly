using Ardalis.Specification;
using Fly.Core.Entities;
using Fly.Core.Pagination;
using Fly.Core.Parameters;

namespace Fly.Core.Specifications;

public class AirportListSpec : Specification<Airport>
{
    public AirportListSpec(AirportParameter parameter, Page page)
    {
        Query.Include(x => x.Aircrafts);

        Query.Include(x => x.City);

        Query.Where(x => parameter.Address == null || x.Address.Contains(parameter.Address));

        Query.Where(x => parameter.CityName == null || x.City.Name.Contains(parameter.CityName));

        Query.Where(x => parameter.AirporId == null || x.AirporId.Contains(parameter.CityName));

        Query.Skip((page.PageNumber - 1) * page.PageSize)
            .Take(page.PageSize).OrderBy(x => x.Id);
    }
}
