using Fly.Core.Entities;
using Fly.Core.Pagination;
using Fly.Core.Parameters;

namespace Fly.WebUI.Models;

public class CitiesViewModel : ViewModelBase
{

    public CityParameter CityParameter { get; set; } = new();
    public PagedResponse<City> PagedResponse { get; set; }

}
