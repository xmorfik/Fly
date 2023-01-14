namespace Fly.Core.Entities;

public class Ticket : BaseEntity
{
    public decimal Price { get; set; }

    public bool IsSold { get; set; }

    public int FlightId { get; set; }
    public Flight? Flight { get; set; }

    public int SeatId { get; set; }
    public Seat? Seat { get; set; }

    public int �lientId { get; set; }
    public �lient �lient { get; set; }
}