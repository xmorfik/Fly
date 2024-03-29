﻿using Fly.Core.Entities;
using Fly.Core.Interfaces;
using Fly.Core.Pagination;
using Fly.Core.Parameters;
using Fly.Core.Services;
using Fly.Core.Specifications;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;

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
            await _repository.DeleteAsync(new Seat() { Id = id });
        }
        catch (Exception ex)
        {
            _logger.LogError(ex.Message);
        }
    }

    public async Task<ResponseBase<Seat>> GetAsync(int id)
    {
        try
        {
            var result = await _repository.FirstOrDefaultAsync(new SeatSpec(id));
            if (result == null)
            {
                return new ResponseBase<Seat>(new Seat()) { Succeeded = false };
            }
            return new ResponseBase<Seat>(result);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex.Message);
            throw;
        }
    }

    public async Task<PagedResponse<Seat>> GetListAsync(SeatParameter parameter, Page page)
    {
        try
        {
            var items = await _repository.ListAsync(new SeatListSpec(parameter, page));
            var count = await _repository.CountAsync(new SeatListSpec(parameter, null));
            var result = new PagedResponse<Seat>(items, count, page);
            return result;
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
        }
    }
}
