using Fly.Core.Entities;
using Fly.Core.Parameters;

namespace Fly.WebUI.Models;

public class AircraftsViewModel : ViewModelBase
{
    public AircraftParameter AircraftParameter { get; set; } = new();
    public List<Aircraft> PagedResponse { get; set; }
}
