namespace Fly.Shared.DataTransferObjects;

public class TicketsDto
{
    public int FlightId { get; set; }

    public decimal BusinessClassPrice { get; set; }

    public decimal FirstClassPrice { get; set; }

    public decimal EconomClassPrice { get; set; }
}
