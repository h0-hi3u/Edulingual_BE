﻿using Edulingual.Api.Controllers.Base;
using Edulingual.Service.Constants;
using Edulingual.Service.Interfaces;
using Edulingual.Service.Request.Role;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Edulingual.Api.Controllers;

[Authorize(Roles = RoleConstants.ADMIN, AuthenticationSchemes = TokenConstants.SCHEMA_BEARER)]
public class RolesController : BaseApiController
{
    private readonly IRoleService _roleService;

    public RolesController(IRoleService roleService)
    {
        _roleService = roleService;
    }

    [HttpGet]
    public async Task<IActionResult> GetAllRolePaging([FromQuery] int pageIndex = 1, [FromQuery] int pageSize = 10)
    {
        return await ExecuteServiceFunc(
            async () => await _roleService.GetAllPaing(pageIndex, pageSize).ConfigureAwait(false)
        ).ConfigureAwait(false);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetRoleById([FromRoute] string id)
    {
        return await ExecuteServiceFunc(
            async () => await _roleService.GetRoleById(id).ConfigureAwait(false)
        ).ConfigureAwait(false);
    }

    [HttpPost]
    public async Task<IActionResult> CreateRole([FromBody] CreateRoleRequest createRoleRequest)
    {
        return await ExecuteServiceFunc(
            async () => await _roleService.CreateRole(createRoleRequest).ConfigureAwait(false)
        ).ConfigureAwait(false);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateRole([FromBody] UpdateRoleRequest updateRoleRequest, [FromRoute] string id)
    {
        return await ExecuteServiceFunc(
            async () => await _roleService.UpdateRole(updateRoleRequest, id).ConfigureAwait(false)
        ).ConfigureAwait(false);
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteRole([FromRoute] string id)
    {
        return await ExecuteServiceFunc(
            async () => await _roleService.DeleteRole(id).ConfigureAwait(false)
        ).ConfigureAwait(false);
    }
}
