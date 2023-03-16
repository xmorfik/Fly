namespace Fly.Core.Parameters;

public class CityParameter
{
    public string? Name { get; set; }

    public string? OrderBy { get; set; } = "Id";

    public bool Descresing { get; set; } = false;
}
