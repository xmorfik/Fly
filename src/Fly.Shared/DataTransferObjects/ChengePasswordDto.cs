using System.ComponentModel.DataAnnotations;

namespace Fly.Shared.DataTransferObjects;

public class ChangePasswordDto
{
    [Required]
    public string? UserName { get; init; }
    [Required]
    public string? Password { get; init; }
    [Required]
    public string? NewPassword { get; init; }
}
