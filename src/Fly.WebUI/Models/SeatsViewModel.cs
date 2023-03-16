using Fly.Core.Entities;
using Fly.Core.Pagination;
using Fly.Core.Parameters;

namespace Fly.WebUI.Models;

public class SeatsViewModel : ViewModelBase
{
    public SeatParameter SeatParameter { get; set; } = new();
    public PagedResponse<Seat> PagedResponse { get; set; }

}
