using Bogus;
using Fly.Core.Entities;

namespace Fly.Tests.Helpers;

public static class FlightHelper
{
    public static List<Flight> Flights = new Faker<Flight>()
                               .RuleFor(x => x.Id, x => x.IndexFaker + 1)
                               .RuleFor(x => x.AircraftId, x => x.IndexFaker + 1)
                               .RuleFor(x => x.DepartureAirportId, x => x.IndexFaker + 1)
                               .RuleFor(x => x.ArrivalAirportId, x => x.IndexFaker + 1)
                               .RuleFor(x => x.DepartureDateTime, x => x.Date.Soon())
                               .RuleFor(x => x.ArrivalDateTime, x => x.Date.Soon())
                               .Generate(15);
}
