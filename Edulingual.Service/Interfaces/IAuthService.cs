using Edulingual.Common.Interfaces;
using Edulingual.Service.Models;
using Edulingual.Service.Request.Authentication;

namespace Edulingual.Service.Interfaces;

public interface IAuthService : ITokenService, IAutoRegisterable
{
    Task<ServiceActionResult> Login(LoginRequest loginRequest);
}
