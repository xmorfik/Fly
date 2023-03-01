using Fly.Core.Entities;
using Fly.Core.Parameters;
using Fly.Core.Services;
using Fly.Services;
using Fly.Shared.DataTransferObjects;

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
        services.AddScoped<IService<Manager, ManagerParameter>, ManagerService>();
        services.AddScoped<IService<Passenger,PassengerParameter>, PassengerService>();
        services.AddScoped<IAircraftLocationService<LocationDto>, AircraftLocationService>();
        services.AddScoped<IScheduleService<Flight>, ScheduleService>();
        services.AddScoped<ITrackingService, TrackingService>();
        services.AddScoped<IRouteBuilder<Flight, LocationDto>, FlightsRouteBuilder>();

        return services;
    }
}
