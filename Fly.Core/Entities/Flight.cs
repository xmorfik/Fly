namespace Fly.Core.Entities;

public class Flight : BaseEntity
{
    public int? DepartureId { get; set; }
    public Airport? Departure { get; set; }

    public int? ArrivalId { get; set; }
    public Airport? Arrival { get; set; }

    public int? AircraftId { get; set; }
    public Aircraft? Aircraft { get; set; }

    public DateTime DepartureDateTime { get; set; }

    public DateTime ArrivalDateTime { get; set; }
}
