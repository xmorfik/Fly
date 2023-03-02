using Ardalis.Specification;
using Fly.Core.Entities;

namespace Fly.Core.Specifications;

public class AircraftLocationSpec : Specification<AircraftLocation>
{
    public AircraftLocationSpec(int flightId, int aircraftId)
    {
        Query.Where(x => x.AircraftId == aircraftId);

        Query.Where(x => x.FlightId == aircraftId);

        Query.OrderByDescending(x => x.DateTime);
    }
}
