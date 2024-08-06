using EduLingual.Common.Models;

namespace Edulingual.Domain.Entities;

public class Question : BaseEntity<Guid>
{
    public string Content { get; set; } = string.Empty;
    public double Point { get; set; } = 0;
    public Guid ExamId { get; set; }
    public virtual Exam Exam { get; set; } = null!;
    public virtual ICollection<Answer> Answers { get; set; } = new List<Answer>();
}
