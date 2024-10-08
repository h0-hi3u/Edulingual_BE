﻿using System.ComponentModel.DataAnnotations;

namespace Edulingual.Service.Request.Role;

public class CreateRoleRequest
{
    [Required(ErrorMessage = "Role name is required!")]
    public string Name { get; set; } = null!;
}
