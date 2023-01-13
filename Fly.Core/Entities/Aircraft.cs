namespace Fly.Core.Entities;

public class Aircraft : BaseEntity
{
    public string? Model { get; set; }

    public int? AirlineId { get; set; }
    public Airline? Airline { get; set; }

    public int? AirportId { get; set; }
    public Airport? Airport { get; set; }

    public ICollection<Seat>? Seats { get; set; }
}