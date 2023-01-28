using Fly.Core.Entities;
using Fly.Core.Interfaces;
using Fly.Core.Pagination;
using Fly.Core.Parameters;
using Fly.Core.Services;
using Fly.Core.Specifications;

namespace Fly.Services;

public class CityService : IService<City, CityParameter>
{
    private readonly IRepository<City> _repository;

    public CityService(IRepository<City> repository)
    {
        _repository = repository;
    }

    public async Task CreateAsync(City item)
    {
        await _repository.AddAsync(item);
    }

    public async Task DeleteAsync(int id)
    {
        await _repository.DeleteAsync(new City() { Id = id });
    }

    public async Task<Response<City>> GetAsync(int id)
    {
        var result = await _repository.FirstOrDefaultAsync(new CitySpec(id));
        if (result == null)
        {
            return new Response<City>(new City()) { Succeeded = false };
        }

        return new Response<City>(result);
    }

    public async Task<PagedResponse<ICollection<City>>> GetListAsync(CityParameter parameter, Page page)
    {
        var items = await _repository.ListAsync(new CityListSpec(parameter, page));

        return new PagedResponse<ICollection<City>>(items, page);
    }

    public async Task UpdateAsync(City item)
    {
        await _repository.UpdateAsync(item);
    }
}
