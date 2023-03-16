namespace Fly.Core.Entities;

public class Message : BaseEntity
{
    public DateTime DateTime { get; set; }

    public string Text { get; set; }

    public string? UserId { get; set; }
    public User? User { get; set; }
}
