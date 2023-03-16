using Fly.Core.Entities;
using Fly.Core.Parameters;

namespace Fly.WebUI.Models;

public class FlightsViewModel : ViewModelBase
{

    public FlightParameter FlightParameter { get; set; } = new();
    public List<Flight> PagedResponse { get; set; }

}
