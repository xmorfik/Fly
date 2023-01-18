namespace Fly.Core.Entities;

public class Airline : BaseEntity
{
    public string? Name { get; set; }

    public string? Phone { get; set; }

    public string? RegistrationAddress { get; set; }

    public string? UserId { get; set; }
    public User? User { get; set; }

    public ICollection<Aircraft>? Aircrafts { get; set; }
}