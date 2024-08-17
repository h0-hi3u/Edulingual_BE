using Edulingual.Api.Controllers.Base;
using Edulingual.Domain.Enum;
using Edulingual.Service.Interfaces;
using Edulingual.Service.Request.User;
using Microsoft.AspNetCore.Mvc;

namespace Edulingual.Api.Controllers;

[Route("api/[controller]")]
[ApiController]
public class UserController : BaseApiController
{
    private readonly IUserService _userService;
    public UserController(IUserService userService)
    {
        _userService = userService;
    }

    [HttpGet("get-self-profile")]
    public async Task<IActionResult> GetSelfProfile()
    {
        return await ExecuteServiceFunc(
            async () => await _userService.GetSelfProfile().ConfigureAwait(false)
            ).ConfigureAwait(false);
    }
    [HttpGet("get-paging-user-role")]
    public async Task<IActionResult> GetPagingUserWithRole([FromQuery] int pageIndex, [FromRoute] int pageSize, [FromRoute] RoleEnum roleValue)
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
    [HttpPut]
    public async Task<IActionResult> UpdateUser(UpdateUserRequest request)
    {
        return await ExecuteServiceFunc(
            async () => await _userService.UpdateUser(request).ConfigureAwait(false)
            ).ConfigureAwait(false);
    }
    [HttpPut("change-status/{id}")]
    public async Task<IActionResult> ChangeStatus([FromRoute] string id, [FromBody] UserStatusEnum status)
    {
        return await ExecuteServiceFunc(
            async () => await _userService.ChangeStatusUser(id, status).ConfigureAwait(false)
            ).ConfigureAwait(false);
    }

}
