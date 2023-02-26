using Fly.Core.Enums;

namespace Fly.Core.Entities;

public class Seat : BaseEntity
{
    public int Row { get; set; }

    public int Column { get; set; }

    public SeatClass SeatClass { get; set; }

    public int? AircraftId { get; set; }
    public Aircraft? Aircraft { get; set; }

    public ICollection<Ticket>? Tickets { get; set; }
}