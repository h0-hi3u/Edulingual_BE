using Edulingual.Api.Controllers.Base;
using Edulingual.Domain.Enum;
using Edulingual.Service.Interfaces;
using Edulingual.Service.Request.Course;
using Edulingual.Service.Request.Search;
using Microsoft.AspNetCore.Mvc;

namespace Edulingual.Api.Controllers;

[Route("api/[controller]")]
[ApiController]
public class CourseController : BaseApiController
{
    private readonly ICourseService _courseSerivce;

    public CourseController(ICourseService courseService)
    {
        _courseSerivce = courseService;
    }

    [HttpPut("change-status/{id}")]
    public async Task<IActionResult> ChangeStatus([FromRoute] string id, [FromBody] CourseStatusEnum status)
    {
        return await ExecuteServiceFunc(
            async() => await _courseSerivce.ChangeStatusCourse(id, status).ConfigureAwait(false)
            ).ConfigureAwait(false);
    }

    [HttpPost("create-course")]
    public async Task<IActionResult> CreateCourse([FromBody] CreateCourseRequest createCourseRequest)
    {
        return await ExecuteServiceFunc(
            async () => await _courseSerivce.CreateCourse(createCourseRequest).ConfigureAwait(false)
            ).ConfigureAwait(false);
    }
    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteCourse([FromRoute] string id)
    {
        return await ExecuteServiceFunc(
            async() => await _courseSerivce.DeleteCourse(id).ConfigureAwait(false)
            ).ConfigureAwait(false);
    }
    [HttpGet("get-paging")]
    public async Task<IActionResult> GetCoursePaging([FromQuery] int pageIndex, [FromQuery] int pageSize)
    {
        return await ExecuteServiceFunc(
            async() => await _courseSerivce.GetCoursePaging(pageIndex, pageSize).ConfigureAwait(false)
            ).ConfigureAwait(false);
    }
    [HttpPost("seach-course")]
    public async Task<IActionResult> SearchCourse([FromBody] SearchCourse searchCourse)
    {
        return await ExecuteServiceFunc(
            async() => await _courseSerivce.SearchCourse(searchCourse).ConfigureAwait(false)
            ).ConfigureAwait(false);
    }
    [HttpPut("update")]
    public async Task<IActionResult> UpdateCourse(UpdateCourseRequest updateCourseRequest)
    {
        return await ExecuteServiceFunc(
            async() => await _courseSerivce.UpdateCourse(updateCourseRequest).ConfigureAwait(false)
            ).ConfigureAwait(false);
    }
}
