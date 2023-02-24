namespace Fly.WebUI.Models;

public class CreateAircraftDto
{
    public string? ModelType { get; set; }

    public string? SerialNumber { get; set; }

    public DateTime? ManufactureDate { get; set; }

    public int? FlightHours { get; set; }

    public int? AirportId { get; set; }

    public int? AirlineId { get; set; }
}
