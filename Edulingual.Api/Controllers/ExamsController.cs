using Edulingual.Api.Controllers.Base;
using Edulingual.Service.Constants;
using Edulingual.Service.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Edulingual.Api.Controllers;

public class ExamsController : BaseApiController
{
    private readonly IExamService _examService;

    public ExamsController(IExamService examService)
    {
        _examService = examService;
    }

    [Authorize]
    [HttpGet("{id}")]
    public async Task<IActionResult> GetExam([FromRoute] string id)
    {
        return await ExecuteServiceFunc(
            async () => await _examService.GetExam(id).ConfigureAwait(false)
            ).ConfigureAwait(false);
    }

    [Authorize(Roles = RoleConstants.TEACHER, AuthenticationSchemes = TokenConstants.SCHEMA_BEARER)]
    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteExam([FromRoute] string id)
    {
        return await ExecuteServiceFunc(
            async () => await _examService.DeleteExam(id).ConfigureAwait(false)
            ).ConfigureAwait(false);
    }

    [Authorize(Roles = RoleConstants.TEACHER, AuthenticationSchemes = TokenConstants.SCHEMA_BEARER)]
    [HttpPost("create-exam-excel/{courseId}")]
    public async Task<IActionResult> CreateExamFromExcel([FromRoute] string courseId, IFormFile file)
    {
        return await ExecuteServiceFunc(
            async () => await _examService.CreateExamFromExcel(courseId, file).ConfigureAwait(false)
            ).ConfigureAwait(false);
    }
}
