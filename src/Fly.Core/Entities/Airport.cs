namespace Fly.Core.Entities;

public class Airport : BaseEntity
{
    public string Name { get; set; }

    public int AirportId { get; set; }

    public double Longitude { get; set; }

    public double Latitude { get; set; }

    public int Altitude { get; set; }

    public int? CityId { get; set; }
    public City? City { get; set; }

    public ICollection<Aircraft>? Aircrafts { get; set; }

    public ICollection<Flight>? FlightsIn { get; set; }

    public ICollection<Flight>? FlightsOut { get; set; }
}