namespace Fly.Core.Entities;

public class Flight : BaseEntity
{
    public Airport Departure { get; set; }
    public Airport Arrival { get; set; }
    public DateTime DepartureDateTime { get; set; }
    public DateTime ArrivalDateTime { get; set; }
    public Aircraft Aircraft { get; set; }
}
