using Fly.Core.Entities;
using Fly.Core.Interfaces;
using Fly.Core.Pagination;
using Fly.Core.Parameters;
using Fly.Core.Services;
using Fly.Core.Specifications;

namespace Fly.Services;

public class AircraftService : IService<Aircraft, AircraftParameter>
{
    private readonly IRepository<Aircraft> _repository;

    public AircraftService(IRepository<Aircraft> repository)
    {
        _repository = repository;
    }

    public async Task CreateAsync(Aircraft item)
    {
        await _repository.AddAsync(item);
    }

    public async Task DeleteAsync(int id)
    {
        await _repository.DeleteAsync(new Aircraft() { Id = id });
    }

    public async Task<Response<Aircraft>> GetAsync(int id)
    {
        var result = await _repository.FirstOrDefaultAsync(new AircraftSpec(id));
        if (result == null)
        {
            return new Response<Aircraft>(new Aircraft()) { Succeeded = false };
        }

        return new Response<Aircraft>(result);
    }

    public async Task<PagedResponse<ICollection<Aircraft>>> GetListAsync(AircraftParameter parameter, Page page)
    {
        var items = await _repository.ListAsync(new AircraftListSpec(parameter, page));

        return new PagedResponse<ICollection<Aircraft>>(items, page);
    }

    public async Task UpdateAsync(Aircraft item)
    {
        await _repository.UpdateAsync(item);
    }
}
