using Fly.Core.Enums;

namespace Fly.Core.Parameters;

public class TicketParameter
{
    public decimal? PriceMax { get; set; }
    public decimal? PriceMin { get; set; }
    public TicketState? TicketState { get; set; }
    public DateTime? DepartureDateTime { get; set; }
    public string? DepartureCity { get; set; }
    public string? ArrivalCity { get; set; }
    public SeatClass? SeatClass { get; set; }
}
