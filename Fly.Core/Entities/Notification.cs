namespace Fly.Core.Entities;

public class Notification : BaseEntity
{
    public string? Header { get; set; }

    public string? Text { get; set; }

    public string? UserId { get; set; }
    public User? User { get; set; }
}
