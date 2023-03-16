namespace Fly.Shared.DataTransferObjects;

public class FlightDto
{
    public int FlightId { get; set; }
    public int AircraftId { get; set; }
    public string AircraftName { get; set; }
    public int DepartureAirportId { get; set; }
    public int ArrivalAirportId { get; set; }
    public double DepartureLatitude { get; set; }
    public double DepartureLongitude { get; set; }
    public double ArrivalLatitude { get; set; }
    public double ArrivalLongitude { get; set; }
    public string DepartureCity { get; set; }
    public string ArrivalCity { get; set; }
    public string DepartureAirport { get; set; }
    public string ArrivalAirport { get; set; }
    public DateTime ArrivalDateTime { get; set; }
    public DateTime DepartureDateTime { get; set; }
}
