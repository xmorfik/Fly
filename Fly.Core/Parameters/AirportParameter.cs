namespace Fly.Core.Parameters;

public class AirportParameter
{
    public string? Name { get; set; }

    public string? CityName { get; set; }

    public string? IsoRegion { get; set; }

    public string? IsoCountry { get; set; }

    public string? OrderBy { get; set; } = "Name";

    public bool Descresing { get; set; } = true;
}
