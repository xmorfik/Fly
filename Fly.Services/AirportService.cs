using Fly.Core.Entities;
using Fly.Core.Interfaces;
using Fly.Core.Pagination;
using Fly.Core.Parameters;
using Fly.Core.Services;
using Fly.Core.Specifications;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;

namespace Fly.Services;

public class AirportService : IService<Airport, AirportParameter>
{
    private readonly IRepository<Airport> _repository;
    private readonly ILogger<AirportService> _logger;

    public AirportService(IRepository<Airport> repository, ILogger<AirportService> logger)
    {
        _repository = repository;
        _logger = logger;
    }

    public async Task CreateAsync(Airport item)
    {
        try
        {
            var result = await _repository.AddAsync(item);
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
            await _repository.DeleteAsync(new Airport() { Id = id });
        }
        catch (Exception ex)
        {
            _logger.LogError(ex.Message);
            throw;
        }
    }

    public async Task<Response<Airport>> GetAsync(int id)
    {
        try
        {
            var result = await _repository.FirstOrDefaultAsync(new AirportSpec(id));
            if (result == null)
            {
                return new Response<Airport>(new Airport()) { Succeeded = false };
            }
            return new Response<Airport>(result);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex.Message);
            throw;
        }
    }

    public async Task<PagedResponse<Airport>> GetListAsync(AirportParameter parameter, Page page)
    {
        try
        {
            var items = await _repository.ListAsync(new AirportListSpec(parameter));
            return PagedResponse<Airport>.ToPagedList(items, page);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex.Message);
            throw;
        }
    }

    public async Task UpdateAsync(Airport item)
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
