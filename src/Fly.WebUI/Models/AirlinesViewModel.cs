using Fly.Core.Entities;
using Fly.Core.Parameters;

namespace Fly.WebUI.Models;

public class AirlinesViewModel : ViewModelBase
{

    public AirlineParameter AirlineParameter { get; set; } = new();
    public List<Airline> PagedResponse { get; set; }

}
