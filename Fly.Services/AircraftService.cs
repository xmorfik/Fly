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
    private readonly ILogger<AircraftService> _logger;

    public AircraftService(IRepository<Aircraft> repository, ILogger<AircraftService> logger)
    {
        _repository = repository;
        _logger = logger;
    }

    public async Task CreateAsync(Aircraft item)
    {
        try
        {
            var result = await _repository.AddAsync(item);
            if(result ==  null)
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
            await _repository.DeleteAsync(new Aircraft() { Id = id });
        }
        catch (Exception ex)
        {
            _logger.LogError(ex.Message);
            throw;
        }
    }

    public async Task<Response<Aircraft>> GetAsync(int id)
    {
        try
        {
            var result = await _repository.FirstOrDefaultAsync(new AircraftSpec(id));
            if (result == null)
            {
                _logger.LogError($"Can't find {id}");
                return new Response<Aircraft>(new Aircraft()) { Succeeded = false };
            }
            return new Response<Aircraft>(result);
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
            var items = await _repository.ListAsync(new AircraftListSpec(parameter));
            if (items == null)
            {
                _logger
                    .LogInformation
                    ("Can't find " + JsonConvert.SerializeObject(parameter) + " , " + JsonConvert.SerializeObject(page));
            }
            return PagedResponse<Aircraft>.ToPagedList(items, page);
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
