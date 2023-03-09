using Fly.Core.Entities;
using Fly.Core.Interfaces;
using Fly.Core.Pagination;
using Fly.Core.Parameters;
using Fly.Core.Services;
using Fly.Core.Specifications;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;

namespace Fly.Services;

public class FlightService : IService<Flight, FlightParameter>
{
    private readonly IRepository<Flight> _repository;
    private readonly IRepository<Aircraft> _aircarftRepository;
    private readonly ILogger<FlightService> _logger;
    private readonly IScheduleService<Flight> _scheduleService;
    private readonly IFlightOnCreationService _flightOnCreationService;

    public FlightService(
        IRepository<Flight> repository,
        ILogger<FlightService> logger,
        IScheduleService<Flight> scheduleService,
        IRepository<Aircraft> aircarftRepository,
        IFlightOnCreationService flightOnCreationService)
    {
        _repository = repository;
        _logger = logger;
        _scheduleService = scheduleService;
        _aircarftRepository = aircarftRepository;
        _flightOnCreationService = flightOnCreationService;
    }

    public async Task CreateAsync(Flight item)
    {
        try
        {
            var canCreate = await _flightOnCreationService.СheckFlight(item);
            if(!canCreate)
            {
                _logger.LogError("Can't create" + JsonConvert.SerializeObject(item));
                return;
            }

            var flight = await _flightOnCreationService.SetDepartureAirport(item);
            if(flight.ArrivalDateTime == null)
            {
                var calculatedFlight = await _flightOnCreationService.SetArrivalDateTime(flight);
                flight = calculatedFlight;
            }

            var result = await _repository.AddAsync(flight);

            _scheduleService.Schedule(result);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex.Message);
            throw;
        }
    }

    public async Task DeleteAsync(int id)
    {
        try
        {
            await _repository.DeleteAsync(new Flight() { Id = id });
        }
        catch (Exception ex)
        {
            _logger.LogError(ex.Message);
            throw;
        }
    }

    public async Task<ResponseBase<Flight>> GetAsync(int id)
    {
        try
        {
            var result = await _repository.FirstOrDefaultAsync(new FlightSpec(id));
            if (result == null)
            {
                return new ResponseBase<Flight>(new Flight()) { Succeeded = false };
            }
            return new ResponseBase<Flight>(result);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex.Message);
            throw;
        }
    }

    public async Task<PagedResponse<Flight>> GetListAsync(FlightParameter parameter, Page page)
    {
        try
        {
            var items = await _repository.ListAsync(new FlightListSpec(parameter, page));
            var count = await _repository.CountAsync(new FlightListSpec(parameter, null));
            var result = new PagedResponse<Flight>(items, count, page);
            return result;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex.Message);
            throw;
        }
    }

    public async Task UpdateAsync(Flight item)
    {
        try
        {
            await _repository.UpdateAsync(item);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex.Message);
            throw;
        }
    }
}
