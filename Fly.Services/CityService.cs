using Fly.Core.Entities;
using Fly.Core.Interfaces;
using Fly.Core.Pagination;
using Fly.Core.Parameters;
using Fly.Core.Services;
using Fly.Core.Specifications;
using Microsoft.Extensions.Logging;

namespace Fly.Services;

public class CityService : IService<City, CityParameter>
{
    private readonly IRepository<City> _repository;
    private readonly ILogger<CityService> _logger;

    public CityService(IRepository<City> repository, ILogger<CityService> logger)
    {
        _repository = repository;
        _logger = logger;
    }

    public async Task CreateAsync(City item)
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
            await _repository.DeleteAsync(new City() { Id = id });
        }
        catch (Exception ex)
        {
            _logger.LogError(ex.Message);
            throw;
        }
    }

    public async Task<Response<City>> GetAsync(int id)
    {
        try
        {
            var result = await _repository.FirstOrDefaultAsync(new CitySpec(id));
            if (result == null)
            {
                return new Response<City>(new City()) { Succeeded = false };
            }
            return new Response<City>(result);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex.Message);
            throw;
        }
    }

    public async Task<PagedResponse<ICollection<City>>> GetListAsync(CityParameter parameter, Page page)
    {
        try
        {
            var items = await _repository.ListAsync(new CityListSpec(parameter, page));

            return new PagedResponse<ICollection<City>>(items, page);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex.Message);
            throw;
        }
    }

    public async Task UpdateAsync(City item)
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
