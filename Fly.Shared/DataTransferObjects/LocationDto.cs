using Redis.OM.Modeling;

namespace Fly.Shared.DataTransferObjects;

[Document(StorageType = StorageType.Json, Prefixes = new[] { "Location" })]
public class LocationDto
{
    [RedisIdField]
    [Indexed]
    public int? AircraftId { get; set; }
    [Indexed]
    public double? Longitude { get; set; }
    [Indexed]
    public double? Latitude { get; set; }
    [Indexed]
    public int? Altitude { get; set; }
    [Indexed]
    public int? DirectionAngle { get; set; }
    [Indexed]
    public int? Speed { get; set; }
    [Indexed]
    public DateTime? DateTime { get; set; }
}
