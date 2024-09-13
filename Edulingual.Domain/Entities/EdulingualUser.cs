using Microsoft.AspNetCore.Identity;

namespace Edulingual.Domain.Entities;

public partial class EdulingualUser : IdentityUser
{
    public string Username { get; set; }
    public string Password { get; set; }
}
