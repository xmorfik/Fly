using Fly.Core.Entities;
using Fly.Core.Interfaces;
using Fly.Core.Pagination;
using Fly.Core.Parameters;
using Fly.Core.Services;
using Fly.Core.Specifications;
using Microsoft.Extensions.Logging;

namespace Fly.Services;

public class AirlineService : IService<Airline, AirlineParameter>
{
    private readonly IRepository<Airline> _repository;
    private readonly ILogger<AirlineService> _logger;

    public AirlineService(IRepository<Airline> repository, ILogger<AirlineService> logger)
    {
        _repository = repository;
        _logger = logger;
    }

    public async Task CreateAsync(Airline item)
    {
        try
        {
            await _repository.AddAsync(item);
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
            await _repository.DeleteAsync(new Airline() { Id = id });
        }
        catch (Exception ex)
        {
            _logger.LogError(ex.Message);
            throw;
        }
    }

    public async Task<Response<Airline>> GetAsync(int id)
    {
        try
        {
            var result = await _repository.FirstOrDefaultAsync(new AirlineSpec(id));
            if (result == null)
            {
                return new Response<Airline>(new Airline()) { Succeeded = false };
            }
            return new Response<Airline>(result);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex.Message);
            throw;
        }
    }

    public async Task<PagedResponse<ICollection<Airline>>> GetListAsync(AirlineParameter parameter, Page page)
    {
        try
        {
            var items = await _repository.ListAsync(new AirlineListSpec(parameter, page));
            var count = await _repository.CountAsync();
            return new PagedResponse<ICollection<Airline>>(items, count, page);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex.Message);
            throw;
        }
    }

    public async Task UpdateAsync(Airline item)
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
