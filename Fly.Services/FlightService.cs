using Fly.Core.Entities;
using Fly.Core.Interfaces;
using Fly.Core.Pagination;
using Fly.Core.Parameters;
using Fly.Core.Services;
using Fly.Core.Specifications;
using Microsoft.Extensions.Logging;

namespace Fly.Services;

public class FlightService : IService<Flight, FlightParameter>
{
    private readonly IRepository<Flight> _repository;
    private readonly ILogger<FlightService> _logger;
    private readonly ScheduleService _scheduleService;

    public FlightService(
        IRepository<Flight> repository, 
        ILogger<FlightService> logger,
        ScheduleService scheduleService)
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
