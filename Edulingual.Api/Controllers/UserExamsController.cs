using Edulingual.Api.Controllers.Base;
using Edulingual.Service.Interfaces;
using Edulingual.Service.Request.Exam;
using Microsoft.AspNetCore.Mvc;

namespace Edulingual.Api.Controllers;

public class UserExamsController : BaseApiController
{
    private readonly IUserExamService _userExamService;

    public UserExamsController(IUserExamService userExamService)
    {
        _userExamService = userExamService;
    }

    [HttpGet("{id}/my-exams")]
    public async Task<IActionResult> GetExamDone([FromRoute] string id, [FromQuery] int pageIndex = 1, [FromQuery] int pageSize = 10)
    {
        return await ExecuteServiceFunc(
            async () => await _userExamService.GetMyExamDoneInCourse(id, pageIndex, pageSize).ConfigureAwait(false)
            ).ConfigureAwait(false);
    }

    [HttpGet("{id}/exams-in-course")]
    public async Task<IActionResult> GetAllExamInCourse([FromRoute] string id)
    {
        return await ExecuteServiceFunc(
            async () => await _userExamService.GetAllExamInCourse(id).ConfigureAwait(false)
            ).ConfigureAwait(false);
    }

    [HttpPost]
    public async Task<IActionResult> CreateUserExam([FromBody] CreateExamResultRequest createExamResultRequest)
    {
        return await ExecuteServiceFunc(
            async () => await _userExamService.CreateUserExam(createExamResultRequest).ConfigureAwait(false)
            ).ConfigureAwait(false);
    }
}
