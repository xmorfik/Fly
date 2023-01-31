using Fly.Core.Entities;
using Fly.Core.Interfaces;
using Fly.Core.Pagination;
using Fly.Core.Parameters;
using Fly.Core.Services;
using Fly.Core.Specifications;
using Microsoft.Extensions.Logging;

namespace Fly.Services;

public class SeatService : IService<Seat, SeatParameter>
{
    private readonly IRepository<Seat> _repository;
    private readonly ILogger<SeatService> _logger;

    public SeatService(IRepository<Seat> repository, ILogger<SeatService> logger)
    {
        _repository = repository;
        _logger = logger;
    }

    public async Task CreateAsync(Seat item)
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
            await _repository.DeleteAsync(new Seat() { Id = id });
        }
        catch (Exception ex)
        {
            _logger.LogError(ex.Message);
            throw;
        }
    }

    public async Task<Response<Seat>> GetAsync(int id)
    {
        try
        {
            var result = await _repository.FirstOrDefaultAsync(new SeatSpec(id));
            if (result == null)
            {
                return new Response<Seat>(new Seat()) { Succeeded = false };
            }
            return new Response<Seat>(result);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex.Message);
            throw;
        }
    }

    public async Task<PagedResponse<ICollection<Seat>>> GetListAsync(SeatParameter parameter, Page page)
    {
        try
        {
            var items = await _repository.ListAsync(new SeatListSpec(parameter, page));

            return new PagedResponse<ICollection<Seat>>(items, page);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex.Message);
            throw;
        }
    }

    public async Task UpdateAsync(Seat item)
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
