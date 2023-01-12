using Fly.Core.Enums;

namespace Fly.Core.Entities;

public class Seat : BaseEntity
{
    public int Row { get; set; }
    public int Column { get; set; }
    public SeatClass SeatClass { get; set; }
}