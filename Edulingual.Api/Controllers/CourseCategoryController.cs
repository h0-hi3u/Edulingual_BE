using Edulingual.Api.Controllers.Base;
using Edulingual.Service.Interfaces;
using Edulingual.Service.Request.CourseCategory;
using Microsoft.AspNetCore.Mvc;

namespace Edulingual.Api.Controllers;

[Route("api/[controller]")]
[ApiController]
public class CourseCategoryController : BaseApiController
{
    private readonly ICourseCategoryService _courseCateogoryService;

    public CourseCategoryController(ICourseCategoryService courseCategoryService)
    {
        _courseCateogoryService = courseCategoryService;
    }
    [HttpGet("get-all")]
    public async Task<IActionResult> GetAll()
    {
        return await ExecuteServiceFunc(
            async () => await _courseCateogoryService.GetAll().ConfigureAwait(false)
            ).ConfigureAwait(false);
    }
    [HttpGet("get-all-paging")]
    public async Task<IActionResult> GetAllPaging([FromQuery] int pageIndex, [FromQuery] int pageSize)
    {
        return await ExecuteServiceFunc(
            async () => await _courseCateogoryService.GetAllPaging(pageIndex, pageSize).ConfigureAwait(false)
            ).ConfigureAwait(false);
    }
    [HttpGet("{id}")]
    public async Task<IActionResult> GetById([FromRoute] string id)
    {
        return await ExecuteServiceFunc(
            async () => await _courseCateogoryService.GetById(id).ConfigureAwait(false)
            ).ConfigureAwait(false);
    }
    [HttpPost]
    public async Task<IActionResult> CreateCourseCategory(CreateCourseCategoryRequest createCourseCategoryRequest)
    {
        return await ExecuteServiceFunc(
            async () => await _courseCateogoryService.CreateCourseCategory(createCourseCategoryRequest).ConfigureAwait(false)
            ).ConfigureAwait(false);
    }
    [HttpPut]
    public async Task<IActionResult> UpdateCourseCategory(UpdateCourseCategoryRequest updateCourseCategoryRequest)
    {
        return await ExecuteServiceFunc(
            async () => await _courseCateogoryService.UpdateCourseCategory(updateCourseCategoryRequest).ConfigureAwait(false)
            ).ConfigureAwait(false);
    }
    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteCourseCategory([FromQuery] string id)
    {
        return await ExecuteServiceFunc(
            async () => await _courseCateogoryService.DeleteCourseCategory(id).ConfigureAwait(false)
            ).ConfigureAwait(false);
    }
}
