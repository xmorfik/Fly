namespace Fly.WebAPI;

public class PostgresConfiguration
{
    public static readonly string Configuration = "PostgresConfiguration";
    public string Server { get; set; } = "";
    public string Port { get; set; } = "";
    public string Database { get; set; } = "";
    public string User { get; set; } = "";
    public string Password { get; set; } = "";
}
