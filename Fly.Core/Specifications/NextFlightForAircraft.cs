using Ardalis.Specification;
using Fly.Core.Entities;

namespace Fly.Core.Specifications;

public class NextFlightForAircraft : Specification<Flight>, ISingleResultSpecification
{
    public NextFlightForAircraft(int aircraftId)
    {
        Query.OrderByDescending(x => x.Id).Where(x => x.AircraftId == aircraftId);
    }
}
