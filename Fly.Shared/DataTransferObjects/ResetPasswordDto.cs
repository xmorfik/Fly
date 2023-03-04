using System.ComponentModel.DataAnnotations;

namespace Fly.Shared.DataTransferObjects;

public class ResetPasswordDto
{
    [Required]
    public string UserName { get; set; }
}
