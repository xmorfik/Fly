using Fly.Core.Enums;

namespace Fly.Core.DataTransferObjects;

public class AircraftDTO : BaseEntityDTO
{
    public string? ModelType { get; set; }

    public string? SerialNumber { get; set; }

    public string? DateTime { get; set; }

    public DateTime? ManufactureDate { get; set; }

    public int? FlightHours { get; set; }

    public AircraftState? AircraftState { get; set; }

    public int? AirlineId { get; set; }

    public int? AirportId { get; set; }
}