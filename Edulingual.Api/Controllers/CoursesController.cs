﻿using Edulingual.Api.Controllers.Base;
using Edulingual.Service.Constants;
using Edulingual.Service.Interfaces;
using Edulingual.Service.Request.Course;
using Edulingual.Service.Request.Search;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Edulingual.Api.Controllers;

public class CoursesController : BaseApiController
{
    private readonly ICourseService _courseSerivce;

    public CoursesController(ICourseService courseService)
    {
        _courseSerivce = courseService;
    }

    [Authorize(Roles = RoleConstants.ADMIN, AuthenticationSchemes = TokenConstants.SCHEMA_BEARER)]
    [HttpPut("{id}/active")]
    public async Task<IActionResult> ChangeStatus([FromRoute] string id)
    {
        return await ExecuteServiceFunc(
            async () => await _courseSerivce.ChangeStatusCourse(id).ConfigureAwait(false)
            ).ConfigureAwait(false);
    }

    [Authorize(Roles = RoleConstants.TEACHER, AuthenticationSchemes = TokenConstants.SCHEMA_BEARER)]
    [HttpPost]
    public async Task<IActionResult> CreateCourse([FromBody] CreateCourseRequest createCourseRequest)
    {
        return await ExecuteServiceFunc(
            async () => await _courseSerivce.CreateCourse(createCourseRequest).ConfigureAwait(false)
            ).ConfigureAwait(false);
    }

    [Authorize(Roles = RoleConstants.TEACHER, AuthenticationSchemes = TokenConstants.SCHEMA_BEARER)]
    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteCourse([FromRoute] string id)
    {
        return await ExecuteServiceFunc(
            async () => await _courseSerivce.DeleteCourse(id).ConfigureAwait(false)
            ).ConfigureAwait(false);
    }

    [HttpGet]
    public async Task<IActionResult> GetCoursePaging([FromQuery] int pageIndex = 1, [FromQuery] int pageSize = 10)
    {
        return await ExecuteServiceFunc(
            async () => await _courseSerivce.GetCoursePaging(pageIndex, pageSize).ConfigureAwait(false)
            ).ConfigureAwait(false);
    }

    [HttpPost("seach-course")]
    public async Task<IActionResult> SearchCourse([FromBody] SearchCourse searchCourse)
    {
        return await ExecuteServiceFunc(
            async () => await _courseSerivce.SearchCourse(searchCourse).ConfigureAwait(false)
            ).ConfigureAwait(false);
    }

    [Authorize(Roles = RoleConstants.TEACHER, AuthenticationSchemes = TokenConstants.SCHEMA_BEARER)]
    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateCourse(UpdateCourseRequest updateCourseRequest, [FromRoute] string id)
    {
        return await ExecuteServiceFunc(
            async () => await _courseSerivce.UpdateCourse(updateCourseRequest, id).ConfigureAwait(false)
            ).ConfigureAwait(false);
    }

    [Authorize(Roles = RoleConstants.TEACHER, AuthenticationSchemes = TokenConstants.SCHEMA_BEARER)]
    [HttpGet("my-coures")]
    public async Task<IActionResult> GetMyCourses([FromQuery] int pageIndex = 1, [FromQuery] int pageSize = 10)
    {
        return await ExecuteServiceFunc(
            async () => await _courseSerivce.GetMyCourses(pageIndex, pageSize).ConfigureAwait(false)
            ).ConfigureAwait(false);
    }
}
