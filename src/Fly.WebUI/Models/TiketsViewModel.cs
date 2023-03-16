using Fly.Core.Entities;
using Fly.Core.Parameters;

namespace Fly.WebUI.Models;

public class TicketsViewModel : ViewModelBase
{
    public TicketParameter TicketParameter { get; set; } = new();
    public List<Ticket> PagedResponse { get; set; }
}
