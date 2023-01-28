using Fly.Core.Entities;
using Fly.Core.Parameters;
using Fly.Core.Services;
using Fly.Services;

namespace Fly.WebAPI.Extensions;

public static class ConfigureServicesExtension
{
    public static IServiceCollection AddServices(this IServiceCollection services)
    {
        services.AddScoped<IService<Flight, FlightParameter>, FlightService>();
        services.AddScoped<IService<Airline, AirlineParameter>, AirlineService>();
        services.AddScoped<IService<Airport, AirportParameter>, AirportService>();
        services.AddScoped<IService<Aircraft, AircraftParameter>, AircraftService>();
        services.AddScoped<IService<Seat, SeatParameter>, SeatService>();
        services.AddScoped<IService<City, CityParameter>, CityService>();
        services.AddScoped<IService<Ticket, TicketParameter>, TicketService>();

        return services;
    }
}
