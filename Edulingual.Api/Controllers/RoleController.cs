using Edulingual.Api.Controllers.Base;
using Edulingual.Service.Interfaces;
using Edulingual.Service.Request.Role;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Edulingual.Api.Controllers;

[Route("api/[controller]")]
[ApiController]
public class RoleController : BaseApiController
{
    private readonly IRoleService _roleService;

    public RoleController(IRoleService roleService)
    {
        _roleService = roleService;
    }

    [HttpGet("all-paging")]
    public async Task<IActionResult> GetAllRolePaging([FromQuery] int pageIndex, [FromQuery] int pageSize)
    {
        return await ExecuteServiceFunc(
            async () => await _roleService.GetAllPaing(pageIndex, pageSize).ConfigureAwait(false)
        ).ConfigureAwait(false);
    }

    [HttpGet("get-by-id/{id}")]
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

    [HttpPut]
    public async Task<IActionResult> UpdateRole([FromBody] UpdateRoleRequest updateRoleRequest)
    {
        return await ExecuteServiceFunc(
            async () => await _roleService.UpdateRole(updateRoleRequest).ConfigureAwait(false)
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
