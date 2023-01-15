namespace Fly.Core.Entities;

public class Client : BaseEntity
{
    public string? FirstName { get; set; }

    public string? LastName { get; set; }

    public string? Email { get; set; }

    public string? Phone { get; set; }

    public ICollection<Ticket>? Tickets { get; set; }

}
