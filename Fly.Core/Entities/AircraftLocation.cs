namespace Fly.Core.Entities;

public class AircraftLocation : BaseEntity
{
    public double? Longitude { get; set; }

    public double? Latitude { get; set; }

    public bool? IsFlying { get; set; }

    public int? AircraftId { get; set; }
    public Aircraft? Aircraft { get; set; }
}
