using Fly.Core.Entities;
using Fly.Core.Pagination;
using Fly.Core.Parameters;

namespace Fly.WebUI.Models;

public class SeatsViewModel
{
    public MetaData MetaData { get; set; }
    public SeatParameter SeatParameter { get; set; } = new();
    public PagedResponse<Seat> PagedResponse { get; set; }
}
