using Microsoft.AspNetCore.Identity;

namespace Fly.Core.Entities;

public class User : IdentityUser
{
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public ICollection<Message>? Messages { get; set; }
    public ICollection<Notification>? Notifications { get; set; }
}
