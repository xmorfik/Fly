namespace Fly.Core.DataTransferObjects;

public class MessageDTO : BaseEntityDTO
{
    public DateTime? DateTime { get; set; }

    public string? Text { get; set; }

    public string? UserId { get; set; }
}
