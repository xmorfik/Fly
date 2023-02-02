using Fly.Core.Entities;
using Fly.Core.Parameters;
using Fly.Core.Services;
using Fly.WebUI.RequestServices;

namespace Fly.WebUI.Extensions;

public static class ConfigureServices
{
    public static IServiceCollection AddServices(this IServiceCollection services)
    {
        services.AddScoped<IService<Aircraft, AircraftParameter>, AircraftRequestService>();

        return services;
    }
}
