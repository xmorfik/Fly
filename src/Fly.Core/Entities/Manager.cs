namespace Fly.Core.Entities;

public class Manager : BaseEntity
{
    public int? AirlineId { get; set; }
    public Airline? Airline { get; set; }

    public string? UserId { get; set; }
    public User? User { get; set; }
}
