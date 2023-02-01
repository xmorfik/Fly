using Redis.OM.Modeling;

namespace Fly.Shared.DataTransferObjects;

[Document(StorageType = StorageType.Json, Prefixes = new[] { "LocationHistory" })]
public class LocationHistotyDto
{
    [RedisIdField]
    [Indexed]
    public int? AircraftId { get; set; }
    [Indexed]
    public List<LocationDto>? LocationDtos { get; set; }
}
