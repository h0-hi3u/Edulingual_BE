using Microsoft.AspNetCore.Mvc;
using Edulingual.Api.Controllers.Base;
using Edulingual.Service.Interfaces;
using Edulingual.Service.Request.Feedback;
using Edulingual.Service.Constants;
using Microsoft.AspNetCore.Authorization;

namespace Edulingual.Api.Controllers;

[Route("api/[controller]")]
[ApiController]
public class FeedbackController : BaseApiController
{
    private readonly IFeedbachSerivce _feedbackService;

    public FeedbackController(IFeedbachSerivce feedbackService)
    {
        _feedbackService = feedbackService;
    }
    [Authorize(Roles = RoleConstants.STUDENT, AuthenticationSchemes = TokenConstants.SCHEMA_BEARER)]
    [HttpPost]
    public async Task<IActionResult> CreateFeedback(CreateFeedbackRequest createFeedbackRequest)
    {
        return await ExecuteServiceFunc(
            async() => await _feedbackService.CreateFeedback(createFeedbackRequest).ConfigureAwait(false)
            ).ConfigureAwait(false);
    }

    [HttpGet("feedback-of-course/{id}")]
    public async Task<IActionResult> GetFeedbackOfCourse([FromRoute] string id, [FromQuery] int pageIndex, [FromQuery] int pageSize)
    {
        return await ExecuteServiceFunc(
            async() => await _feedbackService.GetFeedbackOfCourse(id, pageIndex, pageSize).ConfigureAwait(false)
            ).ConfigureAwait(false);
    }
    [Authorize(Roles = RoleConstants.STUDENT, AuthenticationSchemes = TokenConstants.SCHEMA_BEARER)]
    [HttpGet("my-feedback-course/{id}")]
    public async Task<IActionResult> GetMyFeedbackInCourse([FromRoute] string id)
    {
        return await ExecuteServiceFunc(
            async() => await _feedbackService.GetMyFeedbackInCourse(id).ConfigureAwait(false)
            ).ConfigureAwait(false);
    }
    [Authorize(Roles = RoleConstants.ADMIN, AuthenticationSchemes = TokenConstants.SCHEMA_BEARER)]
    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteFeedback([FromRoute] string id)
    {
        return await ExecuteServiceFunc(
            async() => await _feedbackService.DeleteFeedback(id).ConfigureAwait(false)
            ).ConfigureAwait(false);
    }
    [Authorize(Roles = RoleConstants.STUDENT, AuthenticationSchemes = TokenConstants.SCHEMA_BEARER)]
    [HttpPut]
    public async Task<IActionResult> UpdateFeedback([FromBody] UpdateFeedbackRequest updateFeedbackRequest)
    {
        return await ExecuteServiceFunc(
            async() => await _feedbackService.UpdateFeedback(updateFeedbackRequest).ConfigureAwait(false)
            ).ConfigureAwait(false);
    }
}
