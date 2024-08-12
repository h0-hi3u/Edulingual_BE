namespace Edulingual.Common.Interfaces;

public interface ICurrentUser
{
    Guid? CurrentUserId();
    string? CurrentUserEmail();
}
