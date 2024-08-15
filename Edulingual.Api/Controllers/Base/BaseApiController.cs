using Edulingual.Common.Exceptions;
using Edulingual.Service.Models;
using Microsoft.AspNetCore.Mvc;

namespace Edulingual.Api.Controllers.Base
{
    [ApiController]
    public abstract class BaseApiController : ControllerBase
    {
        private IActionResult BuildSuccessResult(AppActionResult result)
        {
            return base.Ok(result.Data);
        }

        private IActionResult BuildErrorResult(Exception ex)
        {
            if(ex.GetType().IsAssignableTo(typeof(IInvalidException)))
            {
               return BadRequest(ex.Message);
            } else if (ex.GetType().IsAssignableTo(typeof(INotFoundException)))
            {
                return NotFound(ex.Message);
            } else if (ex.GetType().IsAssignableTo(typeof(IDatabaseException)))
            {
                return Problem(ex.Message);
            }
            return Ok();
        }

        protected async Task<IActionResult> ExecuteServiceFunc(Func<Task<AppActionResult>> serviceFunc)
        {
            return await ExecuteServiceFunc(serviceFunc, null);
        }
        protected async Task<IActionResult> ExecuteServiceFunc(Func<Task<AppActionResult>> serviceFunc, Func<Task<AppActionResult>>? errorHandler)
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
}
