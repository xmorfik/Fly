namespace Fly.Core.Parameters;

public class ManagerParameter
{
    public string? AirlineName { get; set; }

    public string? UserName { get; set; }

    public string? OrderBy { get; set; } = "UserName";

    public bool Descresing { get; set; } = false;
}

