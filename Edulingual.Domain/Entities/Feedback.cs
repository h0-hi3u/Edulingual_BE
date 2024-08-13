using Edulingual.Common.Models;

namespace Edulingual.Domain.Entities;

public class Feedback : Auditable
{
    public Guid UserId { get; set; }
    public Guid CourseId { get; set; }
    public string? Content { get; set; } = string.Empty;
    public int Rating { get; set; }

    public virtual User User { get; set; }
    public virtual Course Course { get; set; }
}
