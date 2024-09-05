using Edulingual.Api.Controllers.Base;
using Edulingual.Domain.Enum;
using Edulingual.Service.Constants;
using Edulingual.Service.Interfaces;
using Edulingual.Service.Request.User;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Edulingual.Api.Controllers;

public class UsersController : BaseApiController
{
    private readonly IUserService _userService;
    public UsersController(IUserService userService)
    {
        _userService = userService;
    }

    [Authorize]
    [HttpGet("get-profile")]
    public async Task<IActionResult> GetSelfProfile()
    {
        return await ExecuteServiceFunc(
            async () => await _userService.GetSelfProfile().ConfigureAwait(false)
            ).ConfigureAwait(false);
    }

    [Authorize(Roles = RoleConstants.ADMIN, AuthenticationSchemes = TokenConstants.SCHEMA_BEARER)]
    [HttpGet("users-with-role")]
    public async Task<IActionResult> GetPagingUserWithRole([FromQuery] int pageIndex, [FromQuery] int pageSize, [FromQuery] RoleEnum roleValue)
    {
        return await ExecuteServiceFunc(
            async () => await _userService.GetUserPagingWithRole(pageIndex, pageSize, roleValue).ConfigureAwait(false)
            ).ConfigureAwait(false);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetUserById(string id)
    {
        return await ExecuteServiceFunc(
            async () => await _userService.GetUserById(id).ConfigureAwait(false)
            ).ConfigureAwait(false);
    }

    [HttpPost]
    public async Task<IActionResult> CreateUser(CreateUserRequest request)
    {
        return await ExecuteServiceFunc(
            async () => await _userService.CreateUser(request).ConfigureAwait(false)
            ).ConfigureAwait(false);
    }

    [Authorize]
    [HttpPut]
    public async Task<IActionResult> UpdateUser(UpdateUserRequest request)
    {
        return await ExecuteServiceFunc(
            async () => await _userService.UpdateUser(request).ConfigureAwait(false)
            ).ConfigureAwait(false);
    }

    [Authorize(Roles = RoleConstants.ADMIN, AuthenticationSchemes = TokenConstants.SCHEMA_BEARER)]
    [HttpPut("{id}/change-status")]
    public async Task<IActionResult> ChangeStatus([FromRoute] string id, [FromBody] UserStatusEnum status)
    {
        return await ExecuteServiceFunc(
            async () => await _userService.ChangeStatusUser(id, status).ConfigureAwait(false)
            ).ConfigureAwait(false);
    }
}
