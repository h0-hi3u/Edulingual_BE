using Edulingual.Common.Models;
using Edulingual.Domain.Enum;

namespace Edulingual.Domain.Entities;

public class UserCourse : Auditable
{
    public Guid UserId { get; set; }
    public Guid CourseId { get; set; }
    public UserCourseStatusEnum Status { get; set; } = UserCourseStatusEnum.Studying;

    public virtual Course Course { get; set; } = null!;
    public virtual User User { get; set; } = null!;
}