using Microsoft.AspNetCore.Identity;

namespace Fly.Core.Entities;

public class User : IdentityUser
{
    public ICollection<Message>? Messages { get; set; }
    public ICollection<Notification>? Notifications { get; set; }
}
