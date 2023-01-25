namespace Fly.Core.DataTransferObjects;

public class AirlineDTO : BaseEntityDTO
{
    public string? Name { get; set; }

    public string? Phone { get; set; }

    public string? RegistrationAddress { get; set; }

    public string? UserId { get; set; }

    public List<AircraftDTO>? Aircrafts { get; set; }
}