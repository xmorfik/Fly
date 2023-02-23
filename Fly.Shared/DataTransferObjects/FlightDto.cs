namespace Fly.Shared.DataTransferObjects;

public class FlightDto
{
    public int FlightId { get; set; }
    public double DepartureLatitude { get; set; }
    public double DepartureLongitude { get; set; }
    public double ArrivalLatitude { get; set; }
    public double ArrivalLongitude { get; set; }
}
