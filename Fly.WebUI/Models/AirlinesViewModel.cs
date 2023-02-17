using Fly.Core.Entities;
using Fly.Core.Pagination;
using Fly.Core.Parameters;

namespace Fly.WebUI.Models;

public class AirlinesViewModel
{
    public MetaData MetaData { get; set; }
    public AirlineParameter AirlineParameter { get; set; } = new();
    public PagedResponse<Airline> PagedResponse { get; set; }
}
