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

    public async Task<ICollection<LocationDto>> GetLocations(int id)
    {
        try
        {
            return await _aircraftLocations.Where(x => x.AircraftId == id).ToListAsync();
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
            return await _aircraftLocations.Where(x => x.Id == 1).ToListAsync();
        }
        catch (Exception ex)
        {
            _logger.LogError(ex.Message);
            throw;
        }
    }
}
