using Fly.Core.Entities;
using Fly.Core.Pagination;
using Fly.Core.Parameters;

namespace Fly.WebUI.Models;

public class AircraftsViewModel
{
    public MetaData MetaData { get; set; }
    public AircraftParameter AircraftParameter { get; set; } = new();
    public PagedResponse<Aircraft> PagedResponse { get; set; }
}
