using Edulingual.Service.Attributes;
using Edulingual.Service.Constants;
using System.ComponentModel.DataAnnotations;

namespace Edulingual.Service.Request.User;

public class UpdateUserRequest
{
    [Required]
    public string Phone { get; set; } = null!;
    [Required]
    public string FullName { get; set; } = null!;
    public string Description { get; set; } = null!;
    public string ImageUrl { get; set; } = null!;
}
