namespace Fly.WebUI.Models;

public class FlightCreateViewModel
{
    public int? DepartureAirportId { get; set; }

    public int? ArrivalAirportId { get; set; }

    public int? AircraftId { get; set; }

    public DateTime? DepartureDateTime { get; set; }

    public DateTime? ArrivalDateTime { get; set; }
}
