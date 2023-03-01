using Fly.Core.Entities;
using Fly.Core.Services;
using Hangfire;
using Microsoft.Extensions.Logging;

namespace Fly.Services;

public class ScheduleService : IScheduleService<Flight>
{
    private readonly ILogger<ScheduleService> _logger;
    private readonly ITrackingService _trakingService;
    public ScheduleService(ILogger<ScheduleService> logger,
        ITrackingService trakingService)
    {
        _logger = logger;
        _trakingService = trakingService;
    }

    public async Task Schedule(Flight flight)
    {
        var departureSpan = ((flight.DepartureDateTime ?? DateTime.MinValue) - DateTime.Now);
        var arrivalSpan = ((flight.ArrivalDateTime ?? DateTime.MinValue) - DateTime.Now);
        var id = flight.Id ?? 0;
        if (arrivalSpan <= TimeSpan.Zero)
        {
            BackgroundJob.Schedule(() => Stop(id), TimeSpan.FromSeconds(2));
            return;
        }
        BackgroundJob.Schedule(() => Start(id), departureSpan);
        BackgroundJob.Schedule(() => Stop(id), arrivalSpan);
    }

    public async Task Start(int id)
    {
        await _trakingService.Track(id);
        _logger.LogInformation($"Flight {id} started {DateTime.Now}");
    }

    public async Task Stop(int id)
    {
        await _trakingService.Stop(id);
        _logger.LogInformation($"Flight {id} stopped {DateTime.Now}");
    }
}
