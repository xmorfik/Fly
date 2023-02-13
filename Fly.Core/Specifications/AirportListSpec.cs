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

        Query.Where(x => parameter.Address == null || x.Address.Contains(parameter.Address));

        Query.Where(x => parameter.CityName == null || x.City.Name.Contains(parameter.CityName));

        Query.Where(x => parameter.IATALocationIdentifier == null || x.IATALocationIdentifier.Contains(parameter.IATALocationIdentifier));
    }
}
