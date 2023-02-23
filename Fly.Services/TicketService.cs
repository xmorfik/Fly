using Fly.Core.Entities;
using Fly.Core.Interfaces;
using Fly.Core.Pagination;
using Fly.Core.Parameters;
using Fly.Core.Services;
using Fly.Core.Specifications;
using Microsoft.Extensions.Logging;

namespace Fly.Services;

public class TicketService : IService<Ticket, TicketParameter>
{
    private readonly IRepository<Ticket> _repository;
    private readonly ILogger<TicketService> _logger;

    public TicketService(IRepository<Ticket> repository, ILogger<TicketService> logger)
    {
        _repository = repository;
        _logger = logger;
    }

    public async Task CreateAsync(Ticket item)
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
            await _repository.DeleteAsync(new Ticket() { Id = id });
        }
        catch (Exception ex)
        {
            _logger.LogError(ex.Message);
            throw;
        }
    }

    public async Task<Response<Ticket>> GetAsync(int id)
    {
        try
        {
            var result = await _repository.FirstOrDefaultAsync(new TicketSpec(id));
            if (result == null)
            {
                return new Response<Ticket>(new Ticket()) { Succeeded = false };
            }
            return new Response<Ticket>(result);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex.Message);
            throw;
        }
    }

    public async Task<PagedResponse<Ticket>> GetListAsync(TicketParameter parameter, Page page)
    {
        try
        {
            var items = await _repository.ListAsync(new TicketListSpec(parameter));
            return PagedResponse<Ticket>.ToPagedList(items, page);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex.Message);
            throw;
        }
    }

    public async Task UpdateAsync(Ticket item)
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
