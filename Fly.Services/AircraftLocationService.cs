using Fly.Core.Services;
using Fly.Shared.DataTransferObjects;
using Microsoft.Extensions.Logging;
using Redis.OM;
using Redis.OM.Searching;

namespace Fly.Services;

public class AircraftLocationService : IAircraftLocationService<LocationDto>
{
    private readonly RedisCollection<LocationDto> _aircraftLocations;
    private readonly RedisConnectionProvider _provider;
    private readonly ILogger<AircraftLocationService> _logger;
    public AircraftLocationService(RedisConnectionProvider provider, ILogger<AircraftLocationService> logger)
    {
        _provider = provider;
        _aircraftLocations = (RedisCollection<LocationDto>)provider.RedisCollection<LocationDto>();
        _logger = logger;
    }

    public async Task CreateAsync(LocationDto item)
    {
        try
        {
            await _aircraftLocations.InsertAsync(item);
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
            var item = new LocationDto() { AircraftId = id };
            await _aircraftLocations.DeleteAsync(item);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex.Message);
            throw;
        }
    }

    public async Task<ICollection<LocationDto>> GetСurrentLocations()
    {
        try
        {
            var locationDtos = await _aircraftLocations.ToListAsync();
            return locationDtos;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex.Message);
            throw;
        }
    }

    public async Task UpdateAsync(LocationDto item)
    {
        try
        {
            await _aircraftLocations.UpdateAsync(item);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex.Message);
            throw;
        }
    }
}
