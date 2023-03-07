using Duende.IdentityServer.Models;
using System.ComponentModel.DataAnnotations;

namespace Fly.WebUI.Models;

public class PayoffViewModel
{
    [Required]
    public int TicketId { get; set; }

    [Required]
    public int PassengerId { get; set; }

    [Required]
    public decimal Amount { get; set; }

    [Required]
    public string Description { get; set; }

    [Required]
    public bool IsSuccessed { get; set; } = true;
}
