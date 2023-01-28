using Fly.Core.Entities;
using Fly.Core.Interfaces;
using Fly.Core.Pagination;
using Fly.Core.Parameters;
using Fly.Core.Services;
using Fly.Core.Specifications;

namespace Fly.Services;

internal class AircraftLocationService : IService<AircraftLocation, AircraftLocationParameter>
{
    private readonly IRepository<AircraftLocation> _repository;

    public AircraftLocationService(IRepository<AircraftLocation> repository)
    {
        _repository = repository;
    }

    public async Task CreateAsync(AircraftLocation item)
    {
        await _repository.AddAsync(item);
    }

    public async Task DeleteAsync(int id)
    {
        await _repository.DeleteAsync(new AircraftLocation() { Id = id });
    }

    public async Task<Response<AircraftLocation>> GetAsync(int id)
    {
        var result = await _repository.FirstOrDefaultAsync(new AircraftLocationSpec(id));
        if (result == null)
        {
            return new Response<AircraftLocation>(new AircraftLocation()) { Succeeded = false };
        }

        return new Response<AircraftLocation>(result);
    }

    public async Task<PagedResponse<ICollection<AircraftLocation>>> GetListAsync(AircraftLocationParameter parameter, Page page)
    {
        var items = await _repository.ListAsync(new AircraftLocationListSpec(parameter, page));

        return new PagedResponse<ICollection<AircraftLocation>>(items, page);
    }

    public async Task UpdateAsync(AircraftLocation item)
    {
        await _repository.UpdateAsync(item);
    }
}