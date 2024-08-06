using System.Security.Claims;

namespace EduLingual.Common.Interface;

public interface ICurrentUser
{
    Guid? CurrentUserId();
    string? CurrentUserEmail();
}
