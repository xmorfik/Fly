using Fly.Core.Services;
using Fly.Shared.DataTransferObjects;
using Microsoft.Extensions.Logging;
using Redis.OM;
using Redis.OM.Searching;

namespace Fly.Services;

public class AircraftLocationService : IAircraftLocationService<LocationDto>
{
    private readonly RedisCollection<LocationDto> _aircraftLocations;
    private readonly RedisCollection<LocationHistotyDto> _aircraftLocationsHistory;
    private readonly RedisConnectionProvider _provider;
    private readonly ILogger<AircraftLocationService> _logger;
    public AircraftLocationService(RedisConnectionProvider provider, ILogger<AircraftLocationService> logger)
    {
        _provider = provider;
        _aircraftLocations = (RedisCollection<LocationDto>)provider.RedisCollection<LocationDto>();
        _aircraftLocationsHistory = (RedisCollection<LocationHistotyDto>)provider.RedisCollection<LocationHistotyDto>();
        _logger = logger;
    }

    public async Task CreateAsync(LocationDto item)
    {
        try
        {
            await _aircraftLocations.InsertAsync(item);
            await _aircraftLocationsHistory.InsertAsync(new LocationHistotyDto()
            {
                AircraftId = item.AircraftId,
                LocationDtos = new List<LocationDto>()
                {
                    item
                }
            });
        }
        catch(Exception ex)
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
            await _aircraftLocationsHistory.DeleteAsync(new LocationHistotyDto()
            {
                AircraftId = item.AircraftId,
            });
        }
        catch(Exception ex)
        {
            _logger.LogError(ex.Message);
            throw;
        }
    }

    public async Task DeleteHistoryAsync(int id)
    {
        try
        {
            await _aircraftLocationsHistory.UpdateAsync(new LocationHistotyDto()
            {
                AircraftId = id,
                LocationDtos = new List<LocationDto>()
            });
        }
        catch (Exception ex)
        {
            _logger.LogError(ex.Message);
            throw;
        }
    }

    public async Task<ICollection<LocationDto>> GetLocations(int id)
    {
        try
        {
            var result = await _aircraftLocationsHistory.FirstOrDefaultAsync(x=>x.AircraftId == id);
            return result.LocationDtos;
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
            return await _aircraftLocations.ToListAsync();
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
            var history = await _aircraftLocationsHistory.FindByIdAsync(item.AircraftId.ToString());
            history.LocationDtos.Add(item);
            await _aircraftLocations.UpdateAsync(item);
            await _aircraftLocationsHistory.UpdateAsync(history);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex.Message);
            throw;
        }
    }
}
