using Fly.Core.Entities;
using Fly.Core.Interfaces;
using Fly.Core.Pagination;
using Fly.Core.Parameters;
using Fly.Core.Services;
using Fly.Core.Specifications;

namespace Fly.Services;

public class ManagerService : IService<Manager, ManagerParameter>
{
    private readonly IRepository<Manager> _repository;

    public ManagerService(IRepository<Manager> repository)
    {
        _repository = repository;
    }

    public async Task CreateAsync(Manager item)
    {
        await _repository.AddAsync(item);
    }

    public async Task DeleteAsync(int id)
    {
        await _repository.DeleteAsync(new Manager() { Id = id });
    }

    public async Task<Response<Manager>> GetAsync(int id)
    {
        var result = await _repository.FirstOrDefaultAsync(new ManagerSpec(id));
        if (result == null)
        {
            return new Response<Manager>(new Manager()) { Succeeded = false };
        }

        return new Response<Manager>(result);
    }

    public async Task<PagedResponse<ICollection<Manager>>> GetListAsync(ManagerParameter parameter, Page page)
    {
        var items = await _repository.ListAsync(new ManagerListSpec(parameter, page));

        return new PagedResponse<ICollection<Manager>>(items, page);
    }

    public async Task UpdateAsync(Manager item)
    {
        await _repository.UpdateAsync(item);
    }
}