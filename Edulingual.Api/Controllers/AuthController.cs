using Microsoft.AspNetCore.Mvc;
using Edulingual.Api.Controllers.Base;
using Edulingual.Service.Interfaces;
using Edulingual.Service.Request.Authentication;

namespace Edulingual.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : BaseApiController
    {
        private readonly IAuthService _authService;

        public AuthController(IAuthService authService)
        {
            _authService = authService;
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginRequest loginRequest)
        {
            return await ExecuteServiceFunc(
                async() => await _authService.Login(loginRequest).ConfigureAwait(false)
                ).ConfigureAwait(false);
        }
    }
}
