using Edulingual.Common.Models;

namespace Edulingual.Domain.Entities;

public class Exam : BaseEntity
{
    public string Title { get; set; } = string.Empty;
    public Guid CourseId { get; set; }
    public int TotalQuestion { get; set; } = 0;

    public virtual Course Course { get; set; } = null!;
    public virtual ICollection<Question> Questions { get; set; } = new List<Question>();
    public virtual ICollection<UserExam> UserExams { get; set; } = new List<UserExam>();
}
