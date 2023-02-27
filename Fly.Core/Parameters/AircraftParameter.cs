using Fly.Core.Enums;

namespace Fly.Core.Parameters;

public class AircraftParameter
{
    public string? ModelType { get; set; }
    public AircraftState? AircraftState { get; set; }
    public string? Airline { get; set; }

    public int? AirlineId { get; set; }
}
