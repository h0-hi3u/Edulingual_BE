namespace Edulingual.Infrastructure;

using Edulingual.Common.Interfaces;
using Microsoft.AspNetCore.Http;
using System.Security.Claims;

public class CurrentUser : ICurrentUser
{
    private readonly IHttpContextAccessor _httpAccessor;
    public CurrentUser(IHttpContextAccessor httpAccessor)
    {
        _httpAccessor = httpAccessor;
    }
    public Guid? CurrentUserId()
    {
        if (Guid.TryParse(_httpAccessor.HttpContext.User.FindFirst(ClaimTypes.NameIdentifier)?.Value, out Guid id)) return id;
        return null;
    }
    public string? CurrentUserEmail()
    {
        return _httpAccessor.HttpContext.User.FindFirst(ClaimTypes.Email)?.Value;
    }
}
