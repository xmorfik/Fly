namespace Fly.Core.Entities;

public class Aircraft : BaseEntity
{
    public string? ModelType { get; set; }

    public string? SerialNumber { get; set; }

    public int? AirlineId { get; set; }
    public Airline? Airline { get; set; }

    public int? AirportId { get; set; }
    public Airport? Airport { get; set; }

    public ICollection<Seat>? Seats { get; set; }

    public ICollection<Flight>? Flights { get; set; }

    public ICollection<AircraftLocation>? AircraftLocations { get; set; }
}