using Edulingual.Service.Interfaces;
using Edulingual.Api.Controllers.Base;
using Microsoft.AspNetCore.Mvc;
using Edulingual.Service.Request.CourseLanguage;

namespace Edulingual.Api.Controllers;

[Route("api/[controller]")]
[ApiController]
public class CourseLanguageController : BaseApiController
{
    private readonly ICourseLanguageService _courseLanguageService;

    public CourseLanguageController(ICourseLanguageService courseAreaService)
    {
        _courseLanguageService = courseAreaService;
    }
    [HttpGet("get-all")]
    public async Task<IActionResult> GetAll()
    {
        return await ExecuteServiceFunc(
            async () => await _courseLanguageService.GetAll().ConfigureAwait(false)
            ).ConfigureAwait(false);
    }
    [HttpGet("get-all-paging")]
    public async Task<IActionResult> GetAllPaging([FromQuery] int pageIndex, [FromQuery] int pageSize)
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
    [HttpPost]
    public async Task<IActionResult> CreateCourseLanguage(CreateCourseLanguageRequest createCourseLanguageRequest)
    {
        return await ExecuteServiceFunc(
            async () => await _courseLanguageService.CreateCourseLanguage(createCourseLanguageRequest).ConfigureAwait(false)
            ).ConfigureAwait(false);
    }
    [HttpPut]
    public async Task<IActionResult> UpdateCourseLanguage(UpdateCourseLanguageRequest updateCourseLanguageRequest)
    {
        return await ExecuteServiceFunc(
            async () => await _courseLanguageService.UpdateCourseLanguage(updateCourseLanguageRequest).ConfigureAwait(false)
            ).ConfigureAwait(false);
    }
    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteCourseLanguage([FromRoute] string id)
    {
        return await ExecuteServiceFunc(
            async () => await _courseLanguageService.DeleteCourseLanguage(id).ConfigureAwait(false)
            ).ConfigureAwait(false);
    }
}
