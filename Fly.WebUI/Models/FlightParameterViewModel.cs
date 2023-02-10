namespace Fly.WebUI.Models;

public class FlightParameterViewModel
{
    public DateTime DepartureDateTime { get; set; }
    public string DepartureCity { get; set; }
    public string ArrivalCity { get; set; }

    public int PageNumber { get; set; }

    public int PageSize { get; set; }
}
