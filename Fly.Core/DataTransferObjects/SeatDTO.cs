using Fly.Core.Enums;

namespace Fly.Core.DataTransferObjects;

public class SeatDTO : BaseEntityDTO
{
    public int? Row { get; set; }

    public int? Column { get; set; }

    public SeatClass? SeatClass { get; set; }

    public int? AircraftId { get; set; }
}