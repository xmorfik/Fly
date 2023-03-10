using Fly.Core.Entities;
using Fly.Core.Interfaces;
using Fly.Core.Pagination;
using Fly.Core.Parameters;
using Fly.Core.Services;
using Fly.Core.Specifications;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;

namespace Fly.Services;

public class AircraftService : IService<Aircraft, AircraftParameter>
{
    private readonly IRepository<Aircraft> _repository;
    private readonly IRepository<Flight> _flights;
    private readonly ILogger<AircraftService> _logger;

    public AircraftService(
        IRepository<Aircraft> repository,
        IRepository<Flight> flights,
        ILogger<AircraftService> logger)
    {
        _repository = repository;
        _logger = logger;
        _flights = flights;
    }

    public async Task CreateAsync(Aircraft item)
    {
        try
        {
            var result = await _repository.AddAsync(item);
            if (result != null)
            {
                _logger.LogInformation($"{JsonConvert.SerializeObject(result)} created");
            }
            else
            {
                _logger.LogError($"Can't create {JsonConvert.SerializeObject(item)}");
            }
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
            await _repository.DeleteAsync(new Aircraft() { Id = id });
        }
        catch (Exception ex)
        {
            _logger.LogError(ex.Message);
            throw;
        }
    }

    public async Task<ResponseBase<Aircraft>> GetAsync(int id)
    {
        try
        {
            var result = await _repository.FirstOrDefaultAsync(new AircraftSpec(id));
            if (result == null)
            {
                return new ResponseBase<Aircraft>(new Aircraft()) { Succeeded = false };
            }
            return new ResponseBase<Aircraft>(result);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex.Message);
            throw;
        }
    }

    public async Task<PagedResponse<Aircraft>> GetListAsync(AircraftParameter parameter, Page page)
    {
        try
        {
            var items = await _repository.ListAsync(new AircraftListSpec(parameter, page));
            var count = await _repository.CountAsync(new AircraftListSpec(parameter, null));
            var result = new PagedResponse<Aircraft>(items, count, page);
            return result;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex.Message);
            throw;
        }
    }

    public async Task UpdateAsync(Aircraft item)
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
