using Fly.Core.Entities;
using Fly.Core.Pagination;
using Fly.Core.Parameters;

namespace Fly.WebUI.Models;

public class FlightsViewModel : ViewModelBase
{

    public FlightParameter FlightParameter { get; set; } = new();
    public PagedResponse<Flight> PagedResponse { get; set; }

}
