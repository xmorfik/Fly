using Fly.Core.Entities;
using Fly.Core.Interfaces;
using Fly.Core.Pagination;
using Fly.Core.Parameters;
using Fly.Core.Services;
using Fly.Core.Specifications;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;

namespace Fly.Services;

public class ManagerService : IService<Manager, ManagerParameter>
{
    private readonly IRepository<Manager> _repository;
    private readonly ILogger<ManagerService> _logger;

    public ManagerService(IRepository<Manager> repository, ILogger<ManagerService> logger)
    {
        _repository = repository;
        _logger = logger;
    }

    public async Task CreateAsync(Manager item)
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
            await _repository.DeleteAsync(new Manager() { Id = id });
        }
        catch (Exception ex)
        {
            _logger.LogError(ex.Message);
            throw;
        }
    }

    public async Task<Response<Manager>> GetAsync(int id)
    {
        try
        {
            var result = await _repository.FirstOrDefaultAsync(new ManagerSpec(id));
            if (result == null)
            {
                return new Response<Manager>(new Manager()) { Succeeded = false };
            }
            return new Response<Manager>(result);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex.Message);
            throw;
        }
    }

    public async Task<PagedResponse<Manager>> GetListAsync(ManagerParameter parameter, Page page)
    {
        try
        {
            var items = await _repository.ListAsync(new ManagerListSpec(parameter));
            return PagedResponse<Manager>.ToPagedList(items, page);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex.Message);
            throw;
        }
    }

    public async Task UpdateAsync(Manager item)
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
