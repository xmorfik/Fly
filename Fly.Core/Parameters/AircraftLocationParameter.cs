namespace Fly.Core.Parameters;

public class AircraftLocationParameter
{
    public string? ModelType { get; set; }

    public string? SerialNumber { get; set; }

    public string? OrderBy { get; set; } = "ModelType";

    public bool Descresing { get; set; } = false;
}
