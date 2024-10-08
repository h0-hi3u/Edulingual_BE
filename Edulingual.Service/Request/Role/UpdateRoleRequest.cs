﻿using System.ComponentModel.DataAnnotations;

namespace Edulingual.Service.Request.Role;

public class UpdateRoleRequest
{
    [Required(ErrorMessage = "Id is required!")]
    public Guid Id { get; set; }
    [Required(ErrorMessage = "Name is required!")]
    public string Name { get; set; } = null!;
}
