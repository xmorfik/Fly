namespace Fly.Core.DataTransferObjects;

public class PassengerDTO : BaseEntityDTO
{
    public string? FirstName { get; set; }

    public string? MiddleName { get; set; }

    public string? LastName { get; set; }

    public string? Email { get; set; }

    public string? Phone { get; set; }

    public string? Address { get; set; }

    public string? Nationality { get; set; }

    public string?  PassportNo { get; set; }

    public string? UserId { get; set; }
}
