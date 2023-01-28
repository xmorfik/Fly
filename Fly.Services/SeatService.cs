using Fly.Core.Entities;
using Fly.Core.Interfaces;
using Fly.Core.Pagination;
using Fly.Core.Parameters;
using Fly.Core.Services;
using Fly.Core.Specifications;

namespace Fly.Services;

public class SeatService : IService<Seat, SeatParameter>
{
    private readonly IRepository<Seat> _repository;

    public SeatService(IRepository<Seat> repository)
    {
        _repository = repository;
    }

    public async Task CreateAsync(Seat item)
    {
        await _repository.AddAsync(item);
    }

    public async Task DeleteAsync(int id)
    {
        await _repository.DeleteAsync(new Seat() { Id = id });
    }

    public async Task<Response<Seat>> GetAsync(int id)
    {
        var result = await _repository.FirstOrDefaultAsync(new SeatSpec(id));
        if (result == null)
        {
            return new Response<Seat>(new Seat()) { Succeeded = false };
        }

        return new Response<Seat>(result);
    }

    public async Task<PagedResponse<ICollection<Seat>>> GetListAsync(SeatParameter parameter, Page page)
    {
        var items = await _repository.ListAsync(new SeatListSpec(parameter, page));

        return new PagedResponse<ICollection<Seat>>(items, page);
    }

    public async Task UpdateAsync(Seat item)
    {
        await _repository.UpdateAsync(item);
    }
}