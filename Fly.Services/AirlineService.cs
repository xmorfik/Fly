using Fly.Core.Entities;
using Fly.Core.Interfaces;
using Fly.Core.Pagination;
using Fly.Core.Parameters;
using Fly.Core.Services;
using Fly.Core.Specifications;

namespace Fly.Services;

public class AirlineService : IService<Airline, AirlineParameter>
{
    private readonly IRepository<Airline> _repository;

    public AirlineService(IRepository<Airline> repository)
    {
        _repository = repository;
    }

    public async Task CreateAsync(Airline item)
    {
        await _repository.AddAsync(item);
    }

    public async Task DeleteAsync(int id)
    {
        await _repository.DeleteAsync(new Airline() { Id = id });
    }

    public async Task<Response<Airline>> GetAsync(int id)
    {
        var result = await _repository.FirstOrDefaultAsync(new AirlineSpec(id));
        if (result == null)
        {
            return new Response<Airline>(new Airline()) { Succeeded = false };
        }

        return new Response<Airline>(result);
    }

    public async Task<PagedResponse<ICollection<Airline>>> GetListAsync(AirlineParameter parameter, Page page)
    {
        var items = await _repository.ListAsync(new AirlineListSpec(parameter, page));

        return new PagedResponse<ICollection<Airline>>(items, page);
    }

    public async Task UpdateAsync(Airline item)
    {
        await _repository.UpdateAsync(item);
    }
}
