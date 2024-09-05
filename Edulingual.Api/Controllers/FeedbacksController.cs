using Edulingual.Api.Controllers.Base;
using Edulingual.Service.Constants;
using Edulingual.Service.Interfaces;
using Edulingual.Service.Request.Feedback;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Edulingual.Api.Controllers;

public class FeedbacksController : BaseApiController
{
    private readonly IFeedbachSerivce _feedbackService;

    public FeedbacksController(IFeedbachSerivce feedbackService)
    {
        _feedbackService = feedbackService;
    }

    [Authorize(Roles = RoleConstants.STUDENT, AuthenticationSchemes = TokenConstants.SCHEMA_BEARER)]
    [HttpPost]
    public async Task<IActionResult> CreateFeedback(CreateFeedbackRequest createFeedbackRequest)
    {
        return await ExecuteServiceFunc(
            async () => await _feedbackService.CreateFeedback(createFeedbackRequest).ConfigureAwait(false)
            ).ConfigureAwait(false);
    }

    [HttpGet("{id}/feedback")]
    public async Task<IActionResult> GetFeedbackOfCourse([FromRoute] string id, [FromQuery] int pageIndex, [FromQuery] int pageSize)
    {
        return await ExecuteServiceFunc(
            async () => await _feedbackService.GetFeedbackOfCourse(id, pageIndex, pageSize).ConfigureAwait(false)
            ).ConfigureAwait(false);
    }

    [Authorize(Roles = RoleConstants.STUDENT, AuthenticationSchemes = TokenConstants.SCHEMA_BEARER)]
    [HttpGet("{id}/my-feedback")]
    public async Task<IActionResult> GetMyFeedbackInCourse([FromRoute] string id)
    {
        return await ExecuteServiceFunc(
            async () => await _feedbackService.GetMyFeedbackInCourse(id).ConfigureAwait(false)
            ).ConfigureAwait(false);
    }

    [Authorize(Roles = RoleConstants.ADMIN, AuthenticationSchemes = TokenConstants.SCHEMA_BEARER)]
    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteFeedback([FromRoute] string id)
    {
        return await ExecuteServiceFunc(
            async () => await _feedbackService.DeleteFeedback(id).ConfigureAwait(false)
            ).ConfigureAwait(false);
    }

    [Authorize(Roles = RoleConstants.STUDENT, AuthenticationSchemes = TokenConstants.SCHEMA_BEARER)]
    [HttpPut]
    public async Task<IActionResult> UpdateFeedback([FromBody] UpdateFeedbackRequest updateFeedbackRequest)
    {
        return await ExecuteServiceFunc(
            async () => await _feedbackService.UpdateFeedback(updateFeedbackRequest).ConfigureAwait(false)
            ).ConfigureAwait(false);
    }
}
