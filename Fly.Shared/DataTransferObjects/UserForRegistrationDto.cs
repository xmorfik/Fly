using System.ComponentModel.DataAnnotations;

namespace Fly.Shared.DataTransferObjects;

public record UserForRegistrationDto
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
    public string? Email { get; init; }
    [Required]
    public string? PhoneNumber { get; init; }
}
