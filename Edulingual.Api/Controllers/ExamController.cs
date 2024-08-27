using Edulingual.Api.Controllers.Base;
using Edulingual.Service.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Edulingual.Api.Controllers;

[Route("api/[controller]")]
[ApiController]
public class ExamController : BaseApiController
{
    private readonly IExamService _examService;

    public ExamController(IExamService examService)
    {
        _examService = examService;
    }
    [HttpGet("full-exam/{id}")]
    public async Task<IActionResult> GetFullExam([FromRoute] string id)
    {
        return await ExecuteServiceFunc(
            async() => await _examService.GetExam(id).ConfigureAwait(false)
            ).ConfigureAwait(false);
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteExam([FromRoute] string id)
    {
        return await ExecuteServiceFunc(
            async() => await _examService.DeleteExam(id).ConfigureAwait(false)
            ).ConfigureAwait(false);
    }

    [HttpPost("create-exam-excel/{id}")]
    public async Task<IActionResult> CreateExamFromExcel([FromRoute] string id, [FromBody] IFormFile file)
    {
        return await ExecuteServiceFunc(
            async() => await _examService.CreateExamFromExcel(id, file).ConfigureAwait(false)
            ).ConfigureAwait(false);
    }
}
