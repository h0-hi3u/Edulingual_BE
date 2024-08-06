using EduLingual.Common.Models;

namespace Edulingual.Domain.Entities;

public class Exam : BaseEntity<Guid>
{
    public string Title { get; set; } = string.Empty;
    public Guid CenterId { get; set; }
    public virtual User Center { get; set; } = null!;
    public int TotalQuestion { get; set; } = 0;
    public Guid CourseId { get; set; }
    public virtual Course Course { get; set; } = null!;

    public ICollection<Question> Questions { get; set; } = new List<Question>();

    public virtual ICollection<User> Users { get; set; } = new List<User>();
    public virtual ICollection<UserExam> UserExams { get; set; } = new List<UserExam>();
}
