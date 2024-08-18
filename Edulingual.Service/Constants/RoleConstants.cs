namespace Edulingual.Service.Constants;

public static class RoleConstants
{
    public const string ADMIN = "Admin";
    public const string TEACHER = "Teacher";
    public const string INTERNAL_USER = ADMIN + "," + TEACHER;
    public const string STUDENT = "Student";
}
