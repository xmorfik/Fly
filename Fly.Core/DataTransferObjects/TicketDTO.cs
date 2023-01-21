using Fly.Core.Enums;

namespace Fly.Core.DataTransferObjects;

public class TicketDTO : BaseEntityDTO
{
    public decimal? Price { get; set; }

    public TiketState? TicketState { get; set; }

    public DateTime? SoldDate { get; set; }

    public int? FlightId { get; set; }

    public int? SeatId { get; set; }

    public int? PassengerId { get; set; }
}