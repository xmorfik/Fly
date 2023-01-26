using Fly.Core.Entities;

namespace Fly.Core.DataTransferObjects;

public class FlightDTO : BaseDTO
{
    public int? DepartureAirportId { get; set; }

    public int? ArrivalAirportId { get; set; }

    public int? AircraftId { get; set; }

    public DateTime? DepartureDateTime { get; set; }

    public DateTime? ArrivalDateTime { get; set; }
}
