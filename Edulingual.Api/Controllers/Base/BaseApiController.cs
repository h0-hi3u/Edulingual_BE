using Edulingual.Common.Exceptions;
using Edulingual.Service.Models;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace Edulingual.Api.Controllers.Base;

[Route("api/[controller]")]
[ApiController]
public abstract class BaseApiController : ControllerBase
{
    private IActionResult BuildSuccessResult(ServiceActionResult result)
    {
        return base.StatusCode(statusCode: (int)result.HttpStatusCode, result.Data);
    }

    private IActionResult BuildErrorResult(Exception ex)
    {
        if (ex.GetType().IsAssignableTo(typeof(IInvalidException)))
        {
            return BadRequest(ex.Message);
        }
        else if (ex.GetType().IsAssignableTo(typeof(INotFoundException)))
        {
            return NotFound(ex.Message);
        }
        else
        {
            return Problem(ex.Message);
        }
    }

    protected async Task<IActionResult> ExecuteServiceFunc(Func<Task<ServiceActionResult>> serviceFunc)
    {
        return await ExecuteServiceFunc(serviceFunc, null);
    }
    protected async Task<IActionResult> ExecuteServiceFunc(Func<Task<ServiceActionResult>> serviceFunc, Func<Task<ServiceActionResult>>? errorHandler)
    {
        try
        {
             var result = await serviceFunc();
            return BuildSuccessResult(result);
        }
        catch (Exception ex)
        {
            if (errorHandler != null) await errorHandler();
            return BuildErrorResult(ex);
        }
    }
}
