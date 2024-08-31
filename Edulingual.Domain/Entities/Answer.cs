using Edulingual.Common.Models;

namespace Edulingual.Domain.Entities;

public class Answer : BaseEntity
{
    public string Content { get; set; } = string.Empty;
    public bool IsCorrect { get; set; } = false;
    public Guid QuestionId { get; set; }

    public virtual Question Question { get; set; } = null!;
}
