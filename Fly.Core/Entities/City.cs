namespace Fly.Core.Entities;

public class City : BaseEntity
{
    public string? Name { get; set; }

    public string? Counrty { get; set; }

    public ICollection<Airport>? Airports { get; set; }
}