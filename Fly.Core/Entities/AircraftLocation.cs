namespace Fly.Core.Entities;

public class AircraftLocation : BaseEntity
{
    public double Longitude { get; set; }

    public double Latitude { get; set; }

    public int Altitude { get; set; }

    public int DirectionAngle { get; set; }

    public int Speed { get; set; }

    public DateTime DateTime { get; set; }

    public int? AircraftId { get; set; }
    public Aircraft? Aircraft { get; set; }

	public int? FlightId { get; set; }
	public Flight? Flight { get; set; }
}
