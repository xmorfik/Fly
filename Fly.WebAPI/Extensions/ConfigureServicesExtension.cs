using Fly.Core.DataTransferObjects;
using Fly.Core.Parameters;
using Fly.Core.Services;
using Fly.Services;

namespace Fly.WebAPI.Extensions;

public static class ConfigureServicesExtension
{
    public static IServiceCollection AddServices(this IServiceCollection services)
    {
        services.AddScoped<IService<FlightDTO>, FlightService>();
        services.AddScoped<IFilter<FlightDTO,FlightParameter>, FlightService>();

        return services;
    }
}
