using System.ComponentModel.DataAnnotations;

namespace Fly.Shared.DataTransferObjects;

public class NewPasswordDto
{
    [Required]
    public string Password { get; set; }
    [Required]
    public string ConfirmPassword { get; set; }

    public string Token { get; set; }

    public string UserName { get; set; }
}
