namespace Fly.Core.Entities;

public class Airline : BaseEntity
{
    public string? Name { get; set; }

    public ICollection<Aircraft>? Aircrafts { get; set;}
}