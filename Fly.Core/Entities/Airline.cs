namespace Fly.Core.Entities;

public class Airline : BaseEntity
{
    public string Name { get; set; }

    public string Phone { get; set; }

    public string RegistrationAddress { get; set; }

    public ICollection<Manager>? Managers { get; set; }

    public ICollection<Aircraft>? Aircrafts { get; set; }
}