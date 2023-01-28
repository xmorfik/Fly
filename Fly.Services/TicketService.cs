using Fly.Core.Entities;
using Fly.Core.Interfaces;
using Fly.Core.Pagination;
using Fly.Core.Parameters;
using Fly.Core.Services;
using Fly.Core.Specifications;

namespace Fly.Services;

public class TicketService : IService<Ticket, TicketParameter>
{
    private readonly IRepository<Ticket> _repository;

    public TicketService(IRepository<Ticket> repository)
    {
        _repository = repository;
    }

    public async Task CreateAsync(Ticket item)
    {
        await _repository.AddAsync(item);
    }

    public async Task DeleteAsync(int id)
    {
        await _repository.DeleteAsync(new Ticket() { Id = id });
    }

    public async Task<Response<Ticket>> GetAsync(int id)
    {
        var result = await _repository.FirstOrDefaultAsync(new TicketSpec(id));
        if (result == null)
        {
            return new Response<Ticket>(new Ticket()) { Succeeded = false };
        }

        return new Response<Ticket>(result);
    }

    public async Task<PagedResponse<ICollection<Ticket>>> GetListAsync(TicketParameter parameter, Page page)
    {
        var items = await _repository.ListAsync(new TicketListSpec(parameter, page));

        return new PagedResponse<ICollection<Ticket>>(items, page);
    }

    public async Task UpdateAsync(Ticket item)
    {
        await _repository.UpdateAsync(item);
    }
}