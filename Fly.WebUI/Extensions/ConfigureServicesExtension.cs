using Fly.Core.Entities;
using Fly.Core.Parameters;
using Fly.Core.Services;
using Fly.WebUI.Services;

namespace Fly.WebUI.Extensions;

public static class ConfigureServices
{
    public static IServiceCollection AddServices(this IServiceCollection services)
    {
        services.AddScoped<IService<Aircraft, AircraftParameter>, AircraftRequestService>();
        services.AddScoped<IService<Airport, AirportParameter>, AirportRequestService>();
        services.AddScoped<IService<Flight, FlightParameter>, FlightRequestService>();

        return services;
    }
}
