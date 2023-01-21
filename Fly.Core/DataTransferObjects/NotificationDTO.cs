namespace Fly.Core.DataTransferObjects;

public class NotificationDTO : BaseEntityDTO
{
    public string? Header { get; set; }

    public string? Text { get; set; }

    public string? UserId { get; set; }
}
