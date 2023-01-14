namespace Fly.Core.Entities;

public class Airport : BaseEntity
{
    public string? Name { get; set; }

    public string? Address { get; set; }

    public ICollection<Aircraft>? Aircrafts { get; set;}

    public ICollection<Flight>? FlightsIn { get; set; }

    public ICollection<Flight>? FlightsOut { get; set; }
}