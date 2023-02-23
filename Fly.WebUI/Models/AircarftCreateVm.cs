using Fly.Core.Enums;

namespace Fly.WebUI.Models;

public class AircarftCreateVm
{
    public string ModelType { get; set; }

    public string SerialNumber { get; set; }

    public DateTime ManufactureDate { get; set; }

    public int FlightHours { get; set; }

    public AircraftState AircraftState { get; set; }

    public int AirportId { get; set; }

}
