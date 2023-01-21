namespace Fly.Core.DataTransferObjects;

public class AirportDTO : BaseEntityDTO
{
    public string? AirporId { get; set; }

    public string? Name { get; set; }

    public string? CityId { get; set; }

    public string? City { get; set; }

    public string? Address { get; set; }
}