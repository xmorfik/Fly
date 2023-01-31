namespace Fly.WebAPI;

public class RedisConfiguration
{
    public static readonly string Configuration = "RedisConfiguration";
    public string Server { get; set; } = "";
    public string Port { get; set; } = "";
}
