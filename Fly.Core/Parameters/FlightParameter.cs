using Fly.Core.Enums;

namespace Fly.Core.Parameters;

public class FlightParameter
{
    public FlightState? FlightState { get; set; }

    public DateTime? DepartureDateTime { get; set; }

    public DateTime? ArrivalDateTime { get; set; }

    public string? DepartureCity { get; set; }

    public string? ArrivalCity { get; set; }

    public string? OrderBy { get; set; } = "FlightState";

    public bool Descresing { get; set; } = true;
}
