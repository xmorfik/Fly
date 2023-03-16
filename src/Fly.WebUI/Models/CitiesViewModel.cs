using Fly.Core.Entities;
using Fly.Core.Parameters;

namespace Fly.WebUI.Models;

public class CitiesViewModel : ViewModelBase
{

    public CityParameter CityParameter { get; set; } = new();
    public List<City> PagedResponse { get; set; }

}
