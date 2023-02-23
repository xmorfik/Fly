using Redis.OM.Modeling;

namespace Fly.Shared.DataTransferObjects;

[Document(StorageType = StorageType.Json, Prefixes = new[] { "LocationHistory" })]
public class LocationHistotyDto
{
    [RedisIdField]
    [Indexed]
    public int? Id { get; set; }
    [Indexed(CascadeDepth = 1)]
    public List<LocationDto>? LocationDtos { get; set; } = new List<LocationDto>();
}
