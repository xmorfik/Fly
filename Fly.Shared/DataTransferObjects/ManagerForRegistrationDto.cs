using System.ComponentModel.DataAnnotations;

namespace Fly.Shared.DataTransferObjects;

public record ManagerForRegistrationDto
{
    [Required]
    public string? FirstName { get; init; }
    [Required]
    public string? LastName { get; init; }
    [Required]
    public string? UserName { get; init; }
    [Required]
    public string? Password { get; init; }
    [Required]
    [RegularExpression("^[^@\\s]+@[^@\\s]+\\.[^@\\s]+$", ErrorMessage = "Enter valid email")]
    public string? Email { get; init; }
    [Required]
    [RegularExpression("^[\\+]?[(]?[0-9]{3}[)]?[-\\s\\.]?[0-9]{3}[-\\s\\.]?[0-9]{4,6}$", ErrorMessage = "Enter valid phone number")]
    public string? PhoneNumber { get; init; }
    [Required]
    public string? AirlineId { get; init; }
}
