using Ardalis.Specification;
using Fly.Core.Entities;

namespace Fly.Core.Specifications;

public class AircraftLocationSpec : Specification<AircraftLocation>
{
    public AircraftLocationSpec(DateTime startDate, DateTime endDate, int aircraftId)
    {
        Query.Where(x => x.AircraftId == aircraftId);

        Query.Where(x => x.DateTime >= startDate);

        Query.Where(x => x.DateTime <= endDate);

        Query.OrderByDescending(x => x.DateTime);
    }
}
