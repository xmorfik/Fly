using Ardalis.Specification;
using Fly.Core.Entities;
using Fly.Core.Parameters;

namespace Fly.Core.Specifications;

public class AirportListSpec : Specification<Airport>
{
    public AirportListSpec(AirportParameter parameter)
    {
        Query.Include(x => x.Aircrafts);

        Query.Include(x => x.City);

        Query.Where(x => parameter.CityName == null || x.City.Name.Contains(parameter.CityName));

        Query.Where(x => parameter.IsoRegion == null || x.City.IsoRegion.Contains(parameter.IsoRegion));

        Query.Where(x => parameter.IsoCountry == null || x.City.IsoCountry.Contains(parameter.IsoCountry));

        Query.Where(x => parameter.Name == null || x.Name.Contains(parameter.Name));
    }
}
