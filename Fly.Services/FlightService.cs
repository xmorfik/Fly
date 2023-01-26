using Fly.Core.Entities;
using Fly.Core.Interfaces;
using Fly.Core.Pagination;
using Fly.Core.Parameters;
using Fly.Core.Services;
using Fly.Core.Specifications;

namespace Fly.Services;

public class FlightService : IService<Flight, FlightParameter>
{
    private readonly IRepository<Flight> _repository;

    public FlightService(IRepository<Flight> repository)
    {
        _repository = repository;
    }

    public async Task CreateAsync(Flight item)
    {
        await _repository.AddAsync(item);
    }

    public async Task DeleteAsync(int id)
    {
        await _repository.DeleteAsync(new Flight() { Id = id });
    }

    public async Task<Response<Flight>> GetAsync(int id)
    {
        var result = await _repository.FirstOrDefaultAsync(new FlightSpec(id));
        if (result == null)
        {
            return new Response<Flight>(new Flight()) { Succeeded = false };
        }

        return new Response<Flight>(result);
    }

    public async Task<PagedResponse<ICollection<Flight>>> GetListAsync(FlightParameter parameter, Page page)
    {
        var items = await _repository.ListAsync(new FlightListSpec( parameter, page));

        return new PagedResponse<ICollection<Flight>>(items, page);
    }

    public async Task UpdateAsync(Flight item)
    {
        await _repository.UpdateAsync(item);  
    }
}