namespace Fly.Core.Entities;

public class Airport : BaseEntity
{
    public string? Name { get; set; }

    public string? Address { get; set; }

    public int? CityId { get; set; }
    public City? City { get; set; }

    public ICollection<Aircraft>? Aircrafts { get; set;}
}