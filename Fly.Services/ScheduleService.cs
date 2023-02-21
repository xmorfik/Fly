using Fly.Core.Entities;
using Hangfire;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fly.Services;

public class ScheduleService
{
    private readonly ILogger<ScheduleService> _logger;
    private readonly TrackingService _trakingService;
    public ScheduleService(ILogger<ScheduleService> logger, 
        TrackingService trakingService)
    {
        _logger = logger;
        _trakingService = trakingService;
    }

    public void Schedule(Flight flight)
    {
        var departureSpan = ( flight.DepartureDateTime - DateTime.Now);
        var arrivalSpan = (flight.ArrivalDateTime - DateTime.Now);
        var id = flight.Id ?? 0;
        BackgroundJob.Schedule(() => StartFlight(id), departureSpan);
        BackgroundJob.Schedule(() => StopFlight(id), arrivalSpan);
    }

    public async Task StartFlight(int id)
    {
        await _trakingService.Track(id);
        _logger.LogInformation($"Flight {id} started {DateTime.Now}");
    }

    public async Task StopFlight(int id)
    {
        await _trakingService.Stop(id);
        _logger.LogInformation($"Flight {id} stopped {DateTime.Now}");
    }
}
