using Fly.Core.Entities;
using Fly.Core.Interfaces;
using Fly.Core.Pagination;
using Fly.Core.Parameters;
using Fly.Core.Services;
using Fly.Core.Specifications;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;

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
            var result = await _repository.AddAsync(item);
            if (result != null)
            {
                _logger.LogInformation($"{JsonConvert.SerializeObject(result)} created");
            }
            else
            {
                _logger.LogError($"Can't create {JsonConvert.SerializeObject(item)}");
            }
        }
        catch (Exception ex)
        {
            _logger.LogError(ex.Message);
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
        }
    }

    public async Task<ResponseBase<Ticket>> GetAsync(int id)
    {
        try
        {
            var result = await _repository.FirstOrDefaultAsync(new TicketSpec(id));
            if (result == null)
            {
                return new ResponseBase<Ticket>(new Ticket()) { Succeeded = false };
            }
            return new ResponseBase<Ticket>(result);
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
            var items = await _repository.ListAsync(new TicketListSpec(parameter, page));
            var count = await _repository.CountAsync(new TicketListSpec(parameter, null));
            var result = new PagedResponse<Ticket>(items, count, page);
            return result;
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
        }
    }
}
