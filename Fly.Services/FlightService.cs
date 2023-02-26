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
    private readonly ILogger<FlightService> _logger;
    private readonly IScheduleService<Flight> _scheduleService;

    public FlightService(
        IRepository<Flight> repository,
        ILogger<FlightService> logger,
        IScheduleService<Flight> scheduleService)
    {
        _repository = repository;
        _logger = logger;
        _scheduleService = scheduleService;
    }

    public async Task CreateAsync(Flight item)
    {
        try
        {
            var result = await _repository.AddAsync(item);
            _scheduleService.Schedule(result);
            if (result == null)
            {
                _logger.LogError("Can't create :" + JsonConvert.SerializeObject(item));
            }
            _logger.LogInformation(JsonConvert.SerializeObject(result) + " created");
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

    public async Task<Response<Flight>> GetAsync(int id)
    {
        try
        {
            var result = await _repository.FirstOrDefaultAsync(new FlightSpec(id));
            if (result == null)
            {
                _logger.LogError($"Can't find {id}");
                return new Response<Flight>(new Flight()) { Succeeded = false };
            }
            return new Response<Flight>(result);
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
            var items = await _repository.ListAsync(new FlightListSpec(parameter));
            if (items == null)
            {
                _logger
                    .LogInformation
                    ("Can't find " + JsonConvert.SerializeObject(parameter) + " , " + JsonConvert.SerializeObject(page));
            }
            return PagedResponse<Flight>.ToPagedList(items, page);
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
