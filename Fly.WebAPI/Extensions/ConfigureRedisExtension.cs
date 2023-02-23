using Fly.Services;
using Redis.OM;

namespace Fly.WebAPI.Extensions;

public static class ConfigureRedisExtension
{
    public static void ConfigureRedis(this IServiceCollection services, IConfiguration configuration)
    {
        var cfg = configuration.GetSection(RedisConfiguration.Configuration).Get<RedisConfiguration>();
        services.AddSingleton(new RedisConnectionProvider($"https://{cfg.Server}:{cfg.Port}"));
        services.AddHostedService<IndexCreationService>();
    }
}
