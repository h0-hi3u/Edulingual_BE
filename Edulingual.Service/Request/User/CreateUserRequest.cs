using Edulingual.Domain.Enum;
using Edulingual.Service.Attributes;
using Edulingual.Service.Constants;
using System.ComponentModel.DataAnnotations;

namespace Edulingual.Service.Request.User;

public class CreateUserRequest
{
    [Required]
    public string Username { get; set; } = null!;
    [Required]
    public string Email { get; set; } = null!;
    [Required]
    public string Phone { get; set; } = null!;
    [Required]
    [MatchesPattern(PasswordConstants.PasswordPattern, PasswordConstants.PasswordPatternErrorMessage)]
    public string Password { get; set; } = null!;
    [Required]
    public string FullName { get; set; } = null!;
    public string Description { get; set; } = null!;
    public string ImageUrl { get; set; } = null!;
}
