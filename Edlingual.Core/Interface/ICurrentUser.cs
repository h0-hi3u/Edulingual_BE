using System.Security.Claims;

namespace Edulingual.Common.Interface;

public interface ICurrentUser
{
    Guid? CurrentUserId();
    string? CurrentUserEmail();
}
