using Fly.Core.Entities;
using Fly.Core.Pagination;
using Fly.Core.Parameters;

namespace Fly.WebUI.Models;

public class TicketsViewModel : ViewModelBase
{
    public TicketParameter TicketParameter { get; set; } = new();
    public PagedResponse<Ticket> PagedResponse { get; set; }
}
