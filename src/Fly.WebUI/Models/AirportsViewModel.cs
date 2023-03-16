using Fly.Core.Entities;
using Fly.Core.Pagination;
using Fly.Core.Parameters;

namespace Fly.WebUI.Models;

public class AirportsViewModel : ViewModelBase
{

    public AirportParameter AirportParameter { get; set; } = new();
    public PagedResponse<Airport> PagedResponse { get; set; }

}
