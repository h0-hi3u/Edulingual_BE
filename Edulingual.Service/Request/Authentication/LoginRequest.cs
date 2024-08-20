using System.ComponentModel.DataAnnotations;

namespace Edulingual.Service.Request.Authentication;

public class LoginRequest
{
    [Required]
    public string Email { get; set; } = null!;
    [Required]
    public string Password { get; set; } = null!;
}
