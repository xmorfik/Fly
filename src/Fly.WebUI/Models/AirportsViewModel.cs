using Fly.Core.Entities;
using Fly.Core.Parameters;

namespace Fly.WebUI.Models;

public class AirportsViewModel : ViewModelBase
{

    public AirportParameter AirportParameter { get; set; } = new();
    public List<Airport> PagedResponse { get; set; }

}
