using Edulingual.Common.Models;

namespace Edulingual.Domain.Entities;

public class UserExam : BaseEntity
{
    public Guid UserId { get; set; }
    public virtual User User { get; set; } = null!;

    public Guid ExamId { get; set; }
    public virtual Exam Exam { get; set; } = null!;

    public double Score { get; set; } = 0;
    public int TotalQuestionRight { get; set; } = 0;
    public int TotalQuestionWrong { get; set; } = 0;
}
