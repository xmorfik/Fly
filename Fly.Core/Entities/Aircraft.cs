using Fly.Core.Enums;

namespace Fly.Core.Entities;

public class Aircraft : BaseEntity
{
    public string? ModelType { get; set; }

    public string? SerialNumber { get; set; }

    public string? DateTime { get; set; }

    public DateTime? ManufactureDate { get; set; }

    public int? FlightHours { get; set; }

    public AircraftState? AircraftState { get; set; }

    public int? AirlineId { get; set; }
    public Airline? Airline { get; set; }

    public int? AirportId { get; set; }
    public Airport? Airport { get; set; }

    public ICollection<Seat>? Seats { get; set; }

    public ICollection<Flight>? Flights { get; set; }

    public ICollection<AircraftLocation>? AircraftLocations { get; set; }
}