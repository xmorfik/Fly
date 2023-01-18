using Fly.Core.Enums;

namespace Fly.Core.Entities;

public class Ticket : BaseEntity
{
    public decimal? Price { get; set; }

    public TiketState? TicketState { get; set; }

    public DateTime? SoldDate { get; set; }

    public int? FlightId { get; set; }
    public Flight? Flight { get; set; }

    public int? SeatId { get; set; }
    public Seat? Seat { get; set; }

    public int? PassengerId { get; set; }
    public Passenger? Passenger { get; set; }
}