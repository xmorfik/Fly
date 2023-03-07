using AutoMapper;
using Fly.Core.Entities;
using Fly.Core.Interfaces;
using Fly.Core.Services;
using Fly.Core.Specifications;
using Fly.Shared.DataTransferObjects;
using Microsoft.Extensions.Logging;
using Redis.OM;
using Redis.OM.Searching;

namespace Fly.Services;

public class AircraftLocationService : IAircraftLocationService<LocationDto>
{
    private readonly IRepository<AircraftLocation> _repository;
    private readonly IRepository<Flight> _repositoryFlights;
    private readonly RedisCollection<LocationDto> _aircraftLocations;
    private readonly RedisConnectionProvider _provider;
    private readonly ILogger<AircraftLocationService> _logger;
    private readonly IMapper _mapper;
    public AircraftLocationService(
        RedisConnectionProvider provider,
        ILogger<AircraftLocationService> logger,
        IRepository<Flight> repositoryFlights,
        IRepository<AircraftLocation> repository,
        IMapper mapper)
    {
        _aircraftLocations = (RedisCollection<LocationDto>)provider.RedisCollection<LocationDto>();
        _provider = provider;
        _logger = logger;
        _mapper = mapper;
        _repository = repository;
        _repositoryFlights = repositoryFlights;
    }

    public async Task CreateAsync(LocationDto item)
    {
        try
        {
            await _aircraftLocations.InsertAsync(item);
            await _repository.AddAsync(_mapper.Map<AircraftLocation>(item));
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
            var item = new LocationDto() { FlightId = id };
            await _aircraftLocations.DeleteAsync(item);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex.Message);
            throw;
        }
    }

    public async Task<IEnumerable<LocationDto>> GetСurrentLocations()
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
            await _repository.AddAsync(_mapper.Map<AircraftLocation>(item));
        }
        catch (Exception ex)
        {
            _logger.LogError(ex.Message);
            throw;
        }
    }
}
