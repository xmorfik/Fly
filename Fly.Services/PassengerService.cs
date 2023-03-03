using Fly.Core.Entities;
using Fly.Core.Interfaces;
using Fly.Core.Pagination;
using Fly.Core.Parameters;
using Fly.Core.Services;
using Fly.Core.Specifications;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;

namespace Fly.Services;

public class PassengerService : IService<Passenger, PassengerParameter>
{
    private readonly IRepository<Passenger> _repository;
    private readonly ILogger<PassengerService> _logger;

    public PassengerService(IRepository<Passenger> repository, ILogger<PassengerService> logger)
    {
        _repository = repository;
        _logger = logger;
    }

    public async Task CreateAsync(Passenger item)
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
        }
    }

    public async Task DeleteAsync(int id)
    {
        try
        {
            await _repository.DeleteAsync(new Passenger() { Id = id });
        }
        catch (Exception ex)
        {
            _logger.LogError(ex.Message);
        }
    }

    public async Task<ResponseBase<Passenger>> GetAsync(int id)
    {
        try
        {
            var result = await _repository.FirstOrDefaultAsync(new PassengerSpec(id));
            if (result == null)
            {
                return new ResponseBase<Passenger>(new Passenger()) { Succeeded = false };
            }
            return new ResponseBase<Passenger>(result);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex.Message);
            throw;
        }
    }

    public async Task<PagedResponse<Passenger>> GetListAsync(PassengerParameter parameter, Page page)
    {
        try
        {
            var items = await _repository.ListAsync(new PassengerListSpec(parameter, page));
            var count = await _repository.CountAsync(new PassengerListSpec(parameter, null));
            var result = new PagedResponse<Passenger>(items, count, page);
            return result;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex.Message);
            throw;
        }
    }

    public async Task UpdateAsync(Passenger item)
    {
        try
        {
            await _repository.UpdateAsync(item);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex.Message);
        }
    }
}
