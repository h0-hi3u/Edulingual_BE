using Edulingual.Api.Controllers.Base;
using Edulingual.Service.Interfaces;
using Edulingual.Service.Request.CourseArea;
using Microsoft.AspNetCore.Mvc;

namespace Edulingual.Api.Controllers;

[Route("api/[controller]")]
[ApiController]
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
    public async Task<IActionResult> GetAllPaging([FromQuery] int pageIndex, [FromQuery] int pageSize)
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
    [HttpPost]
    public async Task<IActionResult> CreateCourseArea(CreateCourseAreaRequest createCourseAreaRequest)
    {
        return await ExecuteServiceFunc(
            async() => await _courseAreaService.CreateCourseArea(createCourseAreaRequest).ConfigureAwait(false)
            ).ConfigureAwait(false);
    }
    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteCourseArea([FromQuery] string id)
    {
        return await ExecuteServiceFunc(
            async() => await _courseAreaService.DeleteCourseArea(id).ConfigureAwait(false)
            ).ConfigureAwait(false);
    }
}
