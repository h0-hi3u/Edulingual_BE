using Edulingual.Common.Models;

namespace Edulingual.Domain.Entities;

public class UserExam : BaseEntity
{
    public double Score { get; set; } = 0;
    public double TotalQuestionRight { get; set; } = 0;
    public Guid UserId { get; set; }
    public Guid ExamId { get; set; }

    public virtual User User { get; set; } = null!;
    public virtual Exam Exam { get; set; } = null!;

}
