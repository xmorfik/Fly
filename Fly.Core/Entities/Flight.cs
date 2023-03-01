using Fly.Core.Enums;

namespace Fly.Core.Entities;

public class Flight : BaseEntity
{
    public FlightState FlightState { get; set; }

    public int? DepartureAirportId { get; set; }
    public Airport? DepartureAirport { get; set; }

    public int? ArrivalAirportId { get; set; }
    public Airport? ArrivalAirport { get; set; }

    public int? AircraftId { get; set; }
    public Aircraft? Aircraft { get; set; }

    public DateTime? DepartureDateTime { get; set; }

    public DateTime? ArrivalDateTime { get; set; }

    public ICollection<Ticket>? Tickets { get; set; }

	public ICollection<AircraftLocation>? AircraftLocations { get; set; }
}
