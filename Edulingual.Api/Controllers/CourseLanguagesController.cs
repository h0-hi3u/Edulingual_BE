﻿using Edulingual.Api.Controllers.Base;
using Edulingual.Service.Constants;
using Edulingual.Service.Interfaces;
using Edulingual.Service.Request.CourseLanguage;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Edulingual.Api.Controllers;

public class CourseLanguagesController : BaseApiController
{
    private readonly ICourseLanguageService _courseLanguageService;

    public CourseLanguagesController(ICourseLanguageService courseAreaService)
    {
        _courseLanguageService = courseAreaService;
    }
    //[HttpGet("get-all")]
    //public async Task<IActionResult> GetAll()
    //{
    //    return await ExecuteServiceFunc(
    //        async () => await _courseLanguageService.GetAll().ConfigureAwait(false)
    //        ).ConfigureAwait(false);
    //}
    [HttpGet]
    public async Task<IActionResult> GetAllPaging([FromQuery] int pageIndex = 1, [FromQuery] int pageSize = 10)
    {
        return await ExecuteServiceFunc(
            async () => await _courseLanguageService.GetAllPaging(pageIndex, pageSize).ConfigureAwait(false)
            ).ConfigureAwait(false);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetById([FromRoute] string id)
    {
        return await ExecuteServiceFunc(
            async () => await _courseLanguageService.GetById(id).ConfigureAwait(false)
            ).ConfigureAwait(false);
    }

    [Authorize(Roles = RoleConstants.ADMIN, AuthenticationSchemes = TokenConstants.SCHEMA_BEARER)]
    [HttpPost]
    public async Task<IActionResult> CreateCourseLanguage(CreateCourseLanguageRequest createCourseLanguageRequest)
    {
        return await ExecuteServiceFunc(
            async () => await _courseLanguageService.CreateCourseLanguage(createCourseLanguageRequest).ConfigureAwait(false)
            ).ConfigureAwait(false);
    }

    [Authorize(Roles = RoleConstants.ADMIN, AuthenticationSchemes = TokenConstants.SCHEMA_BEARER)]
    [HttpPut]
    public async Task<IActionResult> UpdateCourseLanguage(UpdateCourseLanguageRequest updateCourseLanguageRequest)
    {
        return await ExecuteServiceFunc(
            async () => await _courseLanguageService.UpdateCourseLanguage(updateCourseLanguageRequest).ConfigureAwait(false)
            ).ConfigureAwait(false);
    }

    [Authorize(Roles = RoleConstants.ADMIN, AuthenticationSchemes = TokenConstants.SCHEMA_BEARER)]
    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteCourseLanguage([FromRoute] string id)
    {
        return await ExecuteServiceFunc(
            async () => await _courseLanguageService.DeleteCourseLanguage(id).ConfigureAwait(false)
            ).ConfigureAwait(false);
    }
}
