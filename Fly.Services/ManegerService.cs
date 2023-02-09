using Fly.Core.Entities;
using Fly.Core.Interfaces;
using Fly.Core.Pagination;
using Fly.Core.Parameters;
using Fly.Core.Services;
using Fly.Core.Specifications;
using Microsoft.Extensions.Logging;

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

    public async Task<PagedResponse<ICollection<Manager>>> GetListAsync(ManagerParameter parameter, Page page)
    {
        try
        {
            var items = await _repository.ListAsync(new ManagerListSpec(parameter, page));
            var count = await _repository.CountAsync();
            return new PagedResponse<ICollection<Manager>>(items, count, page);
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
