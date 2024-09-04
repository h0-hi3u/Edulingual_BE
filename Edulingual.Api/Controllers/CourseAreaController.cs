using Edulingual.Api.Controllers.Base;
using Edulingual.Service.Constants;
using Edulingual.Service.Interfaces;
using Edulingual.Service.Request.CourseArea;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Edulingual.Api.Controllers;

public class CourseAreaController : BaseApiController
{
    private readonly ICourseAreaService _courseAreaService;

    public CourseAreaController(ICourseAreaService courseAreaService)
    {
        _courseAreaService = courseAreaService;
    }
    [HttpGet("get-all")]
    public async Task<IActionResult> GetAll()
    {
        return await ExecuteServiceFunc(
            async() => await _courseAreaService.GetAll().ConfigureAwait(false)
            ).ConfigureAwait(false);
    }
    [HttpGet("get-all-paging")]
    public async Task<IActionResult> GetAllPaging([FromQuery] int pageIndex = 1, [FromQuery] int pageSize = 10)
    {
        return await ExecuteServiceFunc(
            async() => await _courseAreaService.GetAllPaging(pageIndex, pageSize).ConfigureAwait(false)
            ).ConfigureAwait(false);
    }
    [HttpGet("{id}")]
    public async Task<IActionResult> GetById([FromRoute] string id) 
    {
        return await ExecuteServiceFunc(
            async() => await _courseAreaService.GetById(id).ConfigureAwait(false)
            ).ConfigureAwait(false);
    }
    [Authorize(Roles = RoleConstants.ADMIN, AuthenticationSchemes = TokenConstants.SCHEMA_BEARER)]
    [HttpPost]
    public async Task<IActionResult> CreateCourseArea(CreateCourseAreaRequest createCourseAreaRequest)
    {
        return await ExecuteServiceFunc(
            async() => await _courseAreaService.CreateCourseArea(createCourseAreaRequest).ConfigureAwait(false)
            ).ConfigureAwait(false);
    }
    [Authorize(Roles = RoleConstants.ADMIN, AuthenticationSchemes = TokenConstants.SCHEMA_BEARER)]
    [HttpPut]
    public async Task<IActionResult> UpdateCourseArea(UpdateCourseAreaRequest updateCourseAreaRequest)
    {
        return await ExecuteServiceFunc(
            async() => await _courseAreaService.UpdateCourseArea(updateCourseAreaRequest).ConfigureAwait(false)
            ).ConfigureAwait(false);
    }
    [Authorize(Roles = RoleConstants.ADMIN, AuthenticationSchemes = TokenConstants.SCHEMA_BEARER)]
    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteCourseArea([FromRoute] string id)
    {
        return await ExecuteServiceFunc(
            async() => await _courseAreaService.DeleteCourseArea(id).ConfigureAwait(false)
            ).ConfigureAwait(false);
    }
}
