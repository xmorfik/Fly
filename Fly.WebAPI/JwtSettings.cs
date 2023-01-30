namespace Fly.WebAPI;

public class JwtSettings
{
    public static readonly string Settings = "JwtSettings";
    public string ValidIssuer { get; set; } = "";
    public string ValidAudience { get; set; } = "";
}
