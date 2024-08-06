using Edulingual.Common.Models;
using Edulingual.Domain.Enum;

namespace Edulingual.Domain.Entities;

public class UserCourse : Auditable
{
    public Guid UserId { get; set; }
    public virtual User User { get; set; } = null!;

    public Guid CourseId { get; set; }
    public virtual Course Course { get; set; } = null!;

    public DateTime FinishedAt { get; set; }
    public UserCourseStatus Status { get; set; } = UserCourseStatus.Studying;
}
