namespace Fly.Core.Entities;

public class Ticket : BaseEntity
{
    public decimal Price { get; set; }
    public Flight Flight { get; set; }
    public Seat Seat { get; set; }
    public bool IsSold { get; set; }
}