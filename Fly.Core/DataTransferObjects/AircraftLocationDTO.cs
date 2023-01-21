namespace Fly.Core.DataTransferObjects;

public class AircraftLocationDTO : BaseEntityDTO
{
    public double? Longitude { get; set; }

    public double? Latitude { get; set; }

    public int? Altitude { get; set; }

    public int? DirectionAngle { get; set; }

    public int? Speed { get; set; }

    public DateTime? DateTime { get; set; }

    public int? AircraftId { get; set; }
}
