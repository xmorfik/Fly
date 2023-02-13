using Fly.Core.Enums;

namespace Fly.WebUI.Models;

public class AircraftCreateViewModel
{
    public string ModelType { get; set; }

    public string SerialNumber { get; set; }

    public DateTime ManufactureDate { get; set; }

    public int FlightHours { get; set; }

    public AircraftState AircraftState { get; set; }

    public int AirlineId { get; set; }

    public int AirportId { get; set; }
}
