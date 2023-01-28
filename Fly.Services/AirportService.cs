using Fly.Core.Entities;
using Fly.Core.Interfaces;
using Fly.Core.Pagination;
using Fly.Core.Parameters;
using Fly.Core.Services;
using Fly.Core.Specifications;

namespace Fly.Services;

public class AirportService : IService<Airport, AirportParameter>
{
    private readonly IRepository<Airport> _repository;

    public AirportService(IRepository<Airport> repository)
    {
        _repository = repository;
    }

    public async Task CreateAsync(Airport item)
    {
        await _repository.AddAsync(item);
    }

    public async Task DeleteAsync(int id)
    {
        await _repository.DeleteAsync(new Airport() { Id = id });
    }

    public async Task<Response<Airport>> GetAsync(int id)
    {
        var result = await _repository.FirstOrDefaultAsync(new AirportSpec(id));
        if (result == null)
        {
            return new Response<Airport>(new Airport()) { Succeeded = false };
        }

        return new Response<Airport>(result);
    }

    public async Task<PagedResponse<ICollection<Airport>>> GetListAsync(AirportParameter parameter, Page page)
    {
        var items = await _repository.ListAsync(new AirportListSpec(parameter, page));

        return new PagedResponse<ICollection<Airport>>(items, page);
    }

    public async Task UpdateAsync(Airport item)
    {
        await _repository.UpdateAsync(item);
    }
}