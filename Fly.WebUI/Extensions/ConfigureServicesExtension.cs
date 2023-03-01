using Fly.Core.Entities;
using Fly.Core.Parameters;
using Fly.Core.Services;
using Fly.Shared.DataTransferObjects;
using Fly.WebUI.Interfaces;
using Fly.WebUI.Services;

namespace Fly.WebUI.Extensions;

public static class ConfigureServices
{
    public static IServiceCollection AddServices(this IServiceCollection services)
    {
        services.AddScoped<IService<Aircraft, AircraftParameter>, AircraftRequestService>();
        services.AddScoped<IService<Airport, AirportParameter>, AirportRequestService>();
        services.AddScoped<IService<Flight, FlightParameter>, FlightRequestService>();
        services.AddScoped<IService<Seat, SeatParameter>, SeatRequestService>();
        services.AddScoped<IService<Ticket, TicketParameter>, TicketRequestService>();
        services.AddScoped<IService<Airline, AirlineParameter>, AirlineRequestService>();
        services.AddScoped<IService<City, CityParameter>, CityRequestService>();
        services.AddScoped<IService<Manager, ManagerParameter>, ManagerRequestService>();
        services.AddScoped<IService<Passenger, PassengerParameter>, PassengerRequestService>();
        services.AddTransient<IParametersParser, ParametersParser>();
        services.AddScoped<IApiHttpClientService, ApiHttpClientService>();
        services.AddScoped<ISeatsGeneratorService<SeatsDto>, SeatsGeneratorService>();
        services.AddScoped<ITicketsGeneratorService<TicketsDto>, TicketsGeneratorService>();

        return services;
    }
}
